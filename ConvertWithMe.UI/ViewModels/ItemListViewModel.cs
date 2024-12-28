using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ConvertWithMe.Core;
using ConvertWithMe.UI.Messengers;
using ConvertWithMe.UI.Models;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using Xabe.FFmpeg;

namespace ConvertWithMe.UI.ViewModels
{
    partial class ItemListViewModel : ObservableObject, IViewModel
    {

        private ObservableCollection<FileItem> fileItems;
        public IEnumerable<FileItem> FileItems => fileItems;

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
            OpenFileDialog dialog = new OpenFileDialog() 
            { 
                Multiselect = true,
                DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
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
                    if (item.PrimaryVideoStream != null && item.PrimaryVideoStream.Codec != VideoCodec.mjpeg.ToString())
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
        private SettingsMetadata ReadMetadata(string file)
        {
            throw new NotImplementedException();
        }
    }
}
