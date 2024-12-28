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
        public Format Format { get; init; }
        public SettingsAudio SettingsAudio { get; init; }
        public SettingsVideo SettingsVideo { get; init; }
    }
    public static class Presets
    {
        public static Preset p1 = new Preset()
        {
            Name = "Windows Media High Quality",
            Format = VideoFormats.wmv,
            SettingsVideo = new SettingsVideo()
            {
                Codec = VideoCodecs.wmv,
                Bitrate = 20,
                FrameRate = -1,
                Width = -1,
                Height = -1,
                EncodingMode = EncodingMode.CBR,
                PFormat = Xabe.FFmpeg.PixelFormat.yuv420p,
                QuailityPreset = Xabe.FFmpeg.ConversionPreset.Medium
            },
            SettingsAudio = new SettingsAudio() 
            {
                Codec = AudioCodecs.wmav2,
                Bitrate = 320,
                SampleRate = 48000
            }
        };

        public static Preset p2 = new Preset()
        {
            Name = "Full HD High Quality",
            Format = VideoFormats.mp4,
            SettingsVideo = new SettingsVideo()
            {
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
                Codec = AudioCodecs.aac,
                Bitrate = 320,
                SampleRate = 48000
            }
        };

        public static Preset p3 = new Preset()
        {
            Name = "Full HD High Quality (NVEC)",
            Format = VideoFormats.mp4,
            SettingsVideo = new SettingsVideo()
            {
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
                Codec = AudioCodecs.aac,
                Bitrate = 320,
                SampleRate = 48000
            }
        };

        public static Preset p4 = new Preset()
        {
            Name = "HD High Quality",
            Format = VideoFormats.mp4,
            SettingsVideo = new SettingsVideo()
            {
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
                Codec = AudioCodecs.aac,
                Bitrate = 320,
                SampleRate = 48000
            }
        };

        public static Preset p5 = new Preset()
        {
            Name = "HD High Quality (NVEC)",
            Format = VideoFormats.mp4,
            SettingsVideo = new SettingsVideo()
            {

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
                Codec = AudioCodecs.aac,
                Bitrate = 320,
                SampleRate = 48000
            }
        };

        public static Preset custom = new Preset()
        {
            Name = "Custom",
        };
        public static Preset[] AllPresets = [p1, p2, p3, p4, p5, custom];
    }
}
