using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private uint encodingQualityVideo = 0;

        [ObservableProperty]
        private uint encodingQualityAudio = 0;

        public SettingsViewModel()
        {
            
        }
    }
}
