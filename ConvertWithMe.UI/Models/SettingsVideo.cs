using ConvertWithMe.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;


namespace ConvertWithMe.UI.Models
{
    public sealed class SettingsVideo : ISettings
    {
        public Core.Definitions.Format Format  { get; set; }
        public Core.Definitions.VideoCodec Codec { get; set; }
        public int Bitrate { get; set; }
        public int FrameRate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public EncodingMode EncodingMode { get; set; }
        public PixelFormat PFormat { get; set; }
        public ConversionPreset QuailityPreset { get; set; }
    }
}
