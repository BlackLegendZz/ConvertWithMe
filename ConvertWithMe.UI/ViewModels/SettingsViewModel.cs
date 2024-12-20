﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ConvertWithMe.UI.Messengers;
using ConvertWithMe.UI.Models;
using Microsoft.Win32;
using System.CodeDom;

namespace ConvertWithMe.UI.ViewModels
{
    partial class SettingsViewModel : ObservableObject, IViewModel
    {
        [ObservableProperty]
        private bool displayMetadata = true;

        [ObservableProperty]
        private bool displayVideo = true;

        [ObservableProperty]
        private bool convertVideo = true;

        [ObservableProperty]
        private bool convertAudio = false;

        [ObservableProperty]
        private bool displayAudio = false;

        [ObservableProperty]
        private SettingsFile? sFile;

        [ObservableProperty]
        private SettingsMetadata? sMetadata;

        [ObservableProperty]
        private SettingsVideo? sVideo;

        [ObservableProperty]
        private SettingsAudio? sAudio;

		public SettingsViewModel()
		{
            WeakReferenceMessenger.Default.Register<TransferSettingsMessage>(this, (r, m) => {
                ISettings? settings = m.Value;
               
                if (settings == null)
                {
                    SFile = null;
                    SMetadata = null;
                    SVideo = null;
                    SAudio = null;
                    return;
                }
                Type sType = settings.GetType();

                if (sType == typeof(SettingsFile))
                {
                    SFile = settings as SettingsFile;
                    return;
                }
                if (sType == typeof(SettingsMetadata))
                {
                    SMetadata = settings as SettingsMetadata;
                    return;
                }
                if (sType == typeof(SettingsVideo))
                {
                    SVideo = settings as SettingsVideo;
                    return;
                }
                if (sType == typeof(SettingsAudio))
                {
                    SAudio = settings as SettingsAudio;
                    return;
                }

                throw new ArgumentException($"The provided settings class of type {sType.FullName} is invalid.");
            });
		}

		[RelayCommand]
        public void UpdateDestination()
        {
            OpenFolderDialog dialog = new OpenFolderDialog()
            {
                Multiselect = false,
                DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };
            if (dialog.ShowDialog() ?? false)
            {
                SFile.DirDest = dialog.SafeFolderName;
            }
        }



        [RelayCommand]
        public void UpdateFilename()
        {

        }


    }
}
