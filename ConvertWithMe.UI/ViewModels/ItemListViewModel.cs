﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConvertWithMe.Core;
using ConvertWithMe.UI.Models;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using Xabe.FFmpeg;

namespace ConvertWithMe.UI.ViewModels
{
    partial class ItemListViewModel : ObservableObject
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

                    fileItems.Add(item);
                }
            }
        }

        [RelayCommand]
        public void RemoveItem(string fileSrc)
        {
            FileItem? toRemove = fileItems.Where(x => Path.Combine(x.SettingsFile.DirSrc, x.SettingsFile.FilenameSrc).Equals(fileSrc)).FirstOrDefault();
            if (toRemove == null)
            {
                throw new KeyNotFoundException($"No element with path {fileSrc}");
            }
            fileItems.Remove(toRemove);
        }

        [RelayCommand]
        public void SelectedItemChanged(string fileSrc)
        {
            FileItem? selectedItem = fileItems.Where(x => Path.Combine(x.SettingsFile.DirSrc, x.SettingsFile.FilenameSrc).Equals(fileSrc)).FirstOrDefault();
            if (selectedItem == null)
            {
                throw new KeyNotFoundException($"No element with path {fileSrc}");
            }
            /* TODO
             * Settings des Items an das Settings ViewModel übergeben
             */
        }
        private SettingsMetadata ReadMetadata(string file)
        {
            throw new NotImplementedException();
        }
    }
}
