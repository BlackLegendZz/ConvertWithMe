using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConvertWithMe.UI.Models;
using Microsoft.Win32;

namespace ConvertWithMe.UI.ViewModels
{
    partial class SettingsViewModel : ObservableObject
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
        private SettingsFile? settingsFile;

        [ObservableProperty]
        private SettingsMetadata? settingsMetadata;

        [ObservableProperty]
        private SettingsVideo? settingVideo;

        [ObservableProperty]
        private SettingsAudio? settingsAudio;

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
                settingsFile.DirDest = dialog.SafeFolderName;
            }
        }

        [RelayCommand]
        public void UpdateFilename()
        {

        }

        public SettingsViewModel()
        {

        }
    }
}
