using ConvertWithMe.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;


namespace ConvertWithMe.UI.Models
{
    public sealed class SettingsVideo
    {
        public Core.Definitions.Format format  {get; set; }
        public Core.Definitions.VideoCodec codec { get; set; }
        public int bitrate { get; set; }
        public int frameRate { get; set; }
        public int sampleRate { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public EncodingMode encodingMode { get; set; }
        public PixelFormat pFormat { get; set; }
        public ConversionPreset quailityPreset { get; set; }
    }
}
