﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ConvertWithMe.Core;
using ConvertWithMe.Core.Definitions;
using ConvertWithMe.UI.Messengers;
using ConvertWithMe.UI.Models;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Events;

namespace ConvertWithMe.UI.ViewModels
{
    partial class ItemListViewModel : ObservableObject
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
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public ItemListViewModel()
        {
            if (!ApplicationPaths.FFmpeg.Equals(FFmpeg.ExecutablesPath))
            {
                FFmpeg.SetExecutablesPath(ApplicationPaths.FFmpeg);
            }

            fileItems = new ObservableCollection<FileItem>();
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
            IsConverting = true;
            cancellationTokenSource = new CancellationTokenSource();    // new token

            FileConversion fc = new FileConversion();
            for (int i = 0; i < fileItems.Count; i++)
            {
                if (AudioFormats.AvailableAudioFormats.Contains(fileItems[i].SettingsFile.Format))
                {
                    await fc.ConvertToAudio(
                        Path.Combine(fileItems[i].SettingsFile.DirSrc, fileItems[i].SettingsFile.FilenameSrc),
                        Path.Combine(fileItems[i].SettingsFile.DirDest, fileItems[i].SettingsFile.FilenameDest),
                        fileItems[i].SettingsFile.Format,
                        fileItems[i].SettingsAudio.Codec,
                        fileItems[i].SettingsAudio.Bitrate,
                        fileItems[i].SettingsAudio.SampleRate,
                        ConversionProgress,
                        cancellationTokenSource.Token
                        );
                }
                else
                {
                    await fc.ConvertToVideo(
                        Path.Combine(fileItems[i].SettingsFile.DirSrc, fileItems[i].SettingsFile.FilenameSrc),
                        Path.Combine(fileItems[i].SettingsFile.DirDest, $"{fileItems[i].SettingsFile.FilenameDest}.{fileItems[i].SettingsFile.Format.Extension}"),
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
                        cancellationTokenSource.Token
                        );
                }
                ProgressPercentage = 0;
            }
            IsConverting = false;
        }

        [RelayCommand]
        public void CancelConversion()
        {
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
