using ConvertWithMe.Core.Definitions;
using ConvertWithMe.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertWithMe.UI
{
    public struct Preset
    {
        public string Name { get; init; }
        public SettingsAudio SettingsAudio { get; init; }
        public SettingsVideo SettingsVideo { get; init; }
    }
    public static class Presets
    {
        public static Preset p1 = new Preset()
        {
            Name = "Windows Media High Quality",
            SettingsVideo = new SettingsVideo()
            {
                Format = VideoFormats.wmv,
                Codec = VideoCodecs.wmv,
                Bitrate = 20000,
                FrameRate = -1,
                Width = -1,
                Height = -1,
                EncodingMode = EncodingMode.CBR,
                PFormat = Xabe.FFmpeg.PixelFormat.yuv420p,
                QuailityPreset = Xabe.FFmpeg.ConversionPreset.Medium
            },
            SettingsAudio = new SettingsAudio() 
            {
                Format = AudioFormats.wma,
                Codec = AudioCodecs.wmav2,
                Bitrate = 320000,
                SampleRate = 48000
            }
        };

        public static Preset p2 = new Preset()
        {
            Name = "Full HD High Quality",
            SettingsVideo = new SettingsVideo()
            {
                Format = VideoFormats.mp4,
                Codec = VideoCodecs.h264,
                Bitrate = 15,
                FrameRate = -1,
                Width = 1920,
                Height = 1080,
                EncodingMode = EncodingMode.VBR,
                PFormat = Xabe.FFmpeg.PixelFormat.yuv420p,
                QuailityPreset = Xabe.FFmpeg.ConversionPreset.Medium
            },
            SettingsAudio = new SettingsAudio()
            {
                Format = AudioFormats.aac,
                Codec = AudioCodecs.aac,
                Bitrate = 320000,
                SampleRate = 48000
            }
        };

        public static Preset p3 = new Preset()
        {
            Name = "Full HD High Quality (NVEC)",
            SettingsVideo = new SettingsVideo()
            {
                Format = VideoFormats.mp4,
                Codec = VideoCodecs.h265,
                Bitrate = 15,
                FrameRate = -1,
                Width = 1920,
                Height = 1080,
                EncodingMode = EncodingMode.VBR,
                PFormat = Xabe.FFmpeg.PixelFormat.yuv420p,
                QuailityPreset = Xabe.FFmpeg.ConversionPreset.Medium
            },
            SettingsAudio = new SettingsAudio()
            {
                Format = AudioFormats.aac,
                Codec = AudioCodecs.aac,
                Bitrate = 320000,
                SampleRate = 48000
            }
        };

        public static Preset p4 = new Preset()
        {
            Name = "HD High Quality",
            SettingsVideo = new SettingsVideo()
            {
                Format = VideoFormats.mp4,
                Codec = VideoCodecs.h264,
                Bitrate = 15,
                FrameRate = -1,
                Width = 1080,
                Height = 720,
                EncodingMode = EncodingMode.VBR,
                PFormat = Xabe.FFmpeg.PixelFormat.yuv420p,
                QuailityPreset = Xabe.FFmpeg.ConversionPreset.Medium
            },
            SettingsAudio = new SettingsAudio()
            {
                Format = AudioFormats.aac,
                Codec = AudioCodecs.aac,
                Bitrate = 320000,
                SampleRate = 48000
            }
        };

        public static Preset p5 = new Preset()
        {
            Name = "HD High Quality (NVEC)",
            SettingsVideo = new SettingsVideo()
            {
                Format = VideoFormats.mp4,
                Codec = VideoCodecs.h264,
                Bitrate = 15,
                FrameRate = -1,
                Width = 1080,
                Height = 720,
                EncodingMode = EncodingMode.VBR,
                PFormat = Xabe.FFmpeg.PixelFormat.yuv420p,
                QuailityPreset = Xabe.FFmpeg.ConversionPreset.Medium
            },
            SettingsAudio = new SettingsAudio()
            {
                Format = AudioFormats.aac,
                Codec = AudioCodecs.aac,
                Bitrate = 320000,
                SampleRate = 48000
            }
        };

        public static Preset[] AllPresets = [p1, p2, p3, p4, p5];
    }
}
