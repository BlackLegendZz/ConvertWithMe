
using CommunityToolkit.Mvvm.ComponentModel;
using ConvertWithMe.Core.Definitions;

namespace ConvertWithMe.UI.Models
{
    public partial class SettingsAudio : ObservableObject, ISettings
    {
        [ObservableProperty]
        private AudioCodec codec;
        [ObservableProperty]
        private int bitrate;
        [ObservableProperty]
        private int sampleRate;
        
    }
}
