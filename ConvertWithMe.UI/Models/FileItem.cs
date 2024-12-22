using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace ConvertWithMe.UI.Models
{
    public sealed class FileItem
    {
        public SettingsAudio SettingsAudio { get; set; }
        public SettingsFile SettingsFile { get; set; }
        public SettingsMetadata SettingsMetadata { get; set; }
        public SettingsVideo SettingsVideo { get; set; }
        public IAudioStream? PrimaryAudioStream { get; set; }
        public IVideoStream? PrimaryVideoStream { get; set; }

        public bool IsAudioFile
        {
            get
            {
                if (PrimaryVideoStream != null)
                {
                    return PrimaryVideoStream.Codec == VideoCodec.mjpeg.ToString();
                }
                return true;
            }
        }
        public TimeSpan Duration
        {
            get {
                if (PrimaryAudioStream != null && PrimaryVideoStream != null)
                {
                    return PrimaryAudioStream.Duration > PrimaryVideoStream.Duration ? PrimaryAudioStream.Duration : PrimaryVideoStream.Duration;
                }
                else
                {
                    return TimeSpan.Zero;
                }
            }
        }


        public FileItem(SettingsFile settingsFile, SettingsMetadata settingsMetadata, SettingsVideo settingsVideo, SettingsAudio settingsAudio, IAudioStream? audioStream, IVideoStream? videoStream)
        {
            SettingsFile = settingsFile;
            SettingsMetadata = settingsMetadata;
            SettingsVideo = settingsVideo;
            SettingsAudio = settingsAudio;
            PrimaryAudioStream = audioStream;
            PrimaryVideoStream = videoStream;
        }
    }
}
