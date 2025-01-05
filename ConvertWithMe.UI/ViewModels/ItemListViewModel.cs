using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ConvertWithMe.Core;
using ConvertWithMe.Core.Definitions;
using ConvertWithMe.UI.Messengers;
using ConvertWithMe.UI.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Events;

namespace ConvertWithMe.UI.ViewModels
{
    public partial class ItemListViewModel : ObservableObject
    {

        private ObservableCollection<FileItem> fileItems;
        public IEnumerable<FileItem> FileItems => fileItems;

        [ObservableProperty]
        private string progressMessage = string.Empty;

        [ObservableProperty]
        private float progressPercentage = 0f;

        [ObservableProperty]
        private bool isConverting = false;

        private string[] audiofileFormats = ["*.aac", "*.flac", "*.m4a", "*.mp3", "*.ogg", "*.opus", "*.wav", "*.wma", "*.webm"];
        private string[] videofileFormats = ["*.avm", "*.avi", "*.flv", "*.mp4", "*.m4v", "*.mkv", "*.mov", "*.mpg", "*.mpeg", "*.qt", "*.webm", "*.wmv"];
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly DialogViewModel dialogViewModel;
        private readonly IServiceProvider serviceProvider;
        private bool overrideFile = false;

        public ItemListViewModel(IServiceProvider serviceProvider)
        {
            if (!ApplicationPaths.FFmpeg.Equals(FFmpeg.ExecutablesPath))
            {
                FFmpeg.SetExecutablesPath(ApplicationPaths.FFmpeg);
            }

            fileItems = new ObservableCollection<FileItem>();
            dialogViewModel = serviceProvider.GetRequiredService<DialogViewModel>();
            this.serviceProvider = serviceProvider;
        }

        [RelayCommand]
        public async Task AddFiles()
        {
            string[] f = ["All supported files"];
            OpenFileDialog dialog = new OpenFileDialog() 
            { 
                Multiselect = true,
                DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = $"All valid Files|{string.Join(';', audiofileFormats.Concat(videofileFormats).ToArray())}|Audio Files|{string.Join(';', audiofileFormats)}|Video Files|{string.Join(';', videofileFormats)}"
            };
            if (dialog.ShowDialog() ?? false)
            {
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    string filename = dialog.FileNames[i];
                    // Skip existing files
                    if (fileItems.Where(x => Path.Combine(x.SettingsFile.DirSrc, x.SettingsFile.FilenameSrc).Equals(filename)).Any()) { continue; }
                    SettingsFile settingsFile = new SettingsFile(filename, filename);
                    IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(filename);

                    FileItem item = new FileItem(
                        settingsFile, 
                        new SettingsMetadata(), 
                        new SettingsVideo(), 
                        new SettingsAudio(), 
                        mediaInfo.AudioStreams.FirstOrDefault(), 
                        mediaInfo.VideoStreams.FirstOrDefault()
                        );

                    // Set the current resolution and framrate for video files.
                    if (item.PrimaryVideoStream != null && !item.IsAudioFile)
                    {
                        item.SettingsVideo.Width = item.PrimaryVideoStream.Width;
                        item.SettingsVideo.Height = item.PrimaryVideoStream.Height;
                        item.SettingsVideo.FrameRate = (float)item.PrimaryVideoStream.Framerate;
                    }

                    fileItems.Add(item);
                }
            }
        }

        [RelayCommand]
        public void RemoveItem(SettingsFile fileSettings)
        {
            string fileToDelete = Path.Combine(fileSettings.DirSrc, fileSettings.FilenameSrc);
            FileItem? toRemove = fileItems.Where(x => Path.Combine(x.SettingsFile.DirSrc, x.SettingsFile.FilenameSrc).Equals(fileToDelete)).FirstOrDefault();
            if (toRemove == null)
            {
                throw new KeyNotFoundException($"No element with path {fileToDelete}");
            }
            fileItems.Remove(toRemove);
            
            // Clear the Settings
            WeakReferenceMessenger.Default.Send(new TransferSettingsMessage(null));

        }

        [RelayCommand]
        public void SelectedItemChanged(SettingsFile fileSettings)
        {
            string selectedFile = Path.Combine(fileSettings.DirSrc, fileSettings.FilenameSrc);
            FileItem? selectedItem = fileItems.Where(x => Path.Combine(x.SettingsFile.DirSrc, x.SettingsFile.FilenameSrc).Equals(selectedFile)).FirstOrDefault();
            if (selectedItem == null)
            {
                throw new KeyNotFoundException($"No element with path {selectedFile}");
            }
            
            // Send Settings to Settings ViewModel
            WeakReferenceMessenger.Default.Send(new TransferSettingsMessage(selectedItem));
        }

        [RelayCommand]
        public async Task ConvertFilesAsync() 
        {
            if (IsConverting)
            {
                return;
            }

            IsConverting = true;
            cancellationTokenSource = new CancellationTokenSource();    // new token

            FileConversion fc = new FileConversion();
            try
            {
                for (int i = 0; i < fileItems.Count; i++)
                {
                    string src = Path.Combine(fileItems[i].SettingsFile.DirSrc, fileItems[i].SettingsFile.FilenameSrc);
                    string dest = Path.Combine(fileItems[i].SettingsFile.DirDest, $"{fileItems[i].SettingsFile.FilenameDest}.{fileItems[i].SettingsFile.Format.Extension}");

                    // There is a difference between the destination already existsing (for example due to a previously made conversion)
                    // and the destination being the same as the source. The latter is a different scenario which requires a different procedure
                    // as you cant delete the file you are reading from.
                    if (src.Equals(dest))
                    {
                        await dialogViewModel.ShowDialogAsync(
                            serviceProvider.GetRequiredService<NotificationDialogViewModel>(), 
                            new NotificationViewModelData("Error!", "Cant overwrite the original file. Either choose a different filename OR location.")
                            );
                        break;
                    }

                    if (File.Exists(dest) && !overrideFile)
                    {
                        Question result = await dialogViewModel.ShowDialogAsync(
                            serviceProvider.GetRequiredService<QuestionDialogViewModel>(),
                            new QuestionViewModelData("File already exists", $"The file {dest} already exists. Do you want to overwrite it?")
                            );
                        switch (result)
                        {
                            case Question.Yes:
                                overrideFile = true; 
                                break;
                            case Question.No: 
                                overrideFile = false; 
                                break;
                        }
                    }

                    if (AudioFormats.AvailableAudioFormats.Contains(fileItems[i].SettingsFile.Format))
                    {
                        await fc.ConvertToAudio(
                            src,
                            dest,
                            fileItems[i].SettingsFile.Format,
                            fileItems[i].SettingsAudio.Codec,
                            fileItems[i].SettingsAudio.Bitrate,
                            fileItems[i].SettingsAudio.SampleRate,
                            ConversionProgress,
                            cancellationTokenSource.Token,
                            overrideFile
                            );
                    }
                    else
                    {
                        await fc.ConvertToVideo(
                            src,
                            dest,
                            fileItems[i].SettingsFile.Format,
                            fileItems[i].SettingsVideo.Codec,
                            fileItems[i].SettingsAudio.Codec,
                            fileItems[i].SettingsVideo.Bitrate,
                            fileItems[i].SettingsAudio.Bitrate,
                            fileItems[i].SettingsVideo.FrameRate,
                            fileItems[i].SettingsAudio.SampleRate,
                            fileItems[i].SettingsVideo.Width,
                            fileItems[i].SettingsVideo.Height,
                            fileItems[i].SettingsVideo.EncodingMode,
                            PixelFormat.yuv420p,
                            fileItems[i].SettingsVideo.QuailityPreset,
                            ConversionProgress,
                            cancellationTokenSource.Token,
                            overrideFile
                            );
                    }
                    ProgressPercentage = 0;
                }
            } catch (ArgumentException ex)
            {
                await dialogViewModel.ShowDialogAsync(
                    serviceProvider.GetRequiredService<NotificationDialogViewModel>(), 
                    new NotificationViewModelData("Error!", ex.Message)
                    );
            }
            
            IsConverting = false;
        }

        [RelayCommand]
        public void CancelConversion()
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                return;
            }

            cancellationTokenSource.Cancel();
        }

        private void ConversionProgress(object sender, ConversionProgressEventArgs args)
        {
            ProgressPercentage = (float)Math.Round(100f*args.Duration.TotalSeconds/args.TotalLength.TotalSeconds, 2);
        }

        private SettingsMetadata ReadMetadata(string file)
        {
            throw new NotImplementedException();
        }
    }
}
