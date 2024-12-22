using CommunityToolkit.Mvvm.ComponentModel;
using ConvertWithMe.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;


namespace ConvertWithMe.UI.Models
{
    public partial class SettingsVideo : ObservableObject, ISettings
    {
        [ObservableProperty]
        private Core.Definitions.VideoCodec codec;
        public int Bitrate { get; set; }
        public int FrameRate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public EncodingMode EncodingMode { get; set; }
        public PixelFormat PFormat { get; set; }
        public ConversionPreset QuailityPreset { get; set; }
    }
}
