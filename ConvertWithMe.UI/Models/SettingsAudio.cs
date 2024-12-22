
using CommunityToolkit.Mvvm.ComponentModel;
using ConvertWithMe.Core.Definitions;

namespace ConvertWithMe.UI.Models
{
    public partial class SettingsAudio : ObservableObject, ISettings
    {
        [ObservableProperty]
        private AudioCodec codec;
        public int Bitrate { get; set; }
        public int SampleRate { get; set; }
        
    }
}
