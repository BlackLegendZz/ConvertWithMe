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
        [ObservableProperty]
        private int bitrate;
        [ObservableProperty]
        private int frameRate;
        [ObservableProperty]
        private int width;
        [ObservableProperty]
        private int height;
        [ObservableProperty]
        private EncodingMode encodingMode = EncodingMode.CBR;
        [ObservableProperty]
        private PixelFormat pFormat = PixelFormat.yuv420p;
        [ObservableProperty]
        private ConversionPreset quailityPreset = ConversionPreset.Medium;
    }
}
