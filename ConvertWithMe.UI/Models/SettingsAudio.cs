
using ConvertWithMe.Core.Definitions;

namespace ConvertWithMe.UI.Models
{
    public class SettingsAudio
    {
        public Format format;
        public AudioCodec codec { get; set; }
        public int bitrate { get; set; }
        public int sampleRate { get; set; }
        
    }
}
