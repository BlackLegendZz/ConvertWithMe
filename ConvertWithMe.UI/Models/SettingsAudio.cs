
using ConvertWithMe.Core.Definitions;

namespace ConvertWithMe.UI.Models
{
    public class SettingsAudio : ISettings
    {
        public Format Format;
        public AudioCodec Codec { get; set; }
        public int Bitrate { get; set; }
        public int SampleRate { get; set; }
        
    }
}
