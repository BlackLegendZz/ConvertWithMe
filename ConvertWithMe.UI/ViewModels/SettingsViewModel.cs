using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ConvertWithMe.Core.Definitions;
using ConvertWithMe.UI.Messengers;
using ConvertWithMe.UI.Models;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using Xabe.FFmpeg;

namespace ConvertWithMe.UI.ViewModels
{
    partial class SettingsViewModel : ObservableObject, IViewModel
    {
        [ObservableProperty]
        private bool displayMetadata = true;

        [ObservableProperty]
        private bool displayVideo = false;

        [ObservableProperty]
        private bool convertVideo = false;

        [ObservableProperty]
        private bool convertAudio = false;

        [ObservableProperty]
        private bool displayAudio = false;

        [ObservableProperty]
        private SettingsFile sFile;

        [ObservableProperty]
        private SettingsMetadata sMetadata;

        [ObservableProperty]
        private SettingsVideo sVideo;

        [ObservableProperty]
        private SettingsAudio sAudio;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AvailableFormats))]
        private bool isAudioFile;

        [ObservableProperty]
        private bool isVBRSelected = false;

        [ObservableProperty]
        private bool isFileSelected = false;

        private ObservableCollection<EncodingMode> encodingProfiles = new ObservableCollection<EncodingMode>(Enum.GetValues(typeof(EncodingMode)).Cast<EncodingMode>());
        public IEnumerable<EncodingMode> EncodingProfiles => encodingProfiles;

        private ObservableCollection<Preset> presetList;
        public IEnumerable<Preset> PresetList => presetList;
        public IEnumerable<Core.Definitions.Format> AvailableFormats { 
            get
            {
                if (IsAudioFile)
                {
                    return AudioFormats.AvailableAudioFormats;
                }
                else
                {
                    return VideoFormats.AvailableVideoFormats.Concat(AudioFormats.AvailableAudioFormats);
                }
            } 
        }
        public IEnumerable<int> Framrates => [24, 25, 30, 60];
        public IEnumerable<ConversionPreset> QualityPresets => Enum.GetValues(typeof(ConversionPreset)).Cast<ConversionPreset>();
        private bool presetManuallyChanged;
        private bool isInitialisingFile = false;

        public SettingsViewModel()
		{
            SFile = new SettingsFile(string.Empty, string.Empty);
            SMetadata = new SettingsMetadata();
            SVideo = new SettingsVideo();
            SAudio = new SettingsAudio();

            presetList = new ObservableCollection<Preset>(Presets.AllPresets);
            WeakReferenceMessenger.Default.Register<TransferSettingsMessage>(this, UpdateTransferredSettings);
		}

        [RelayCommand]
        public void UpdateDestination()
        {
            if (SFile == null) {  return; }

            OpenFolderDialog dialog = new OpenFolderDialog()
            {
                Multiselect = false,
                DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };
            if (dialog.ShowDialog() ?? false)
            {
                SFile.DirDest = dialog.SafeFolderName;
            }
        }

        #region Combobox Triggers

        [RelayCommand]
        public void PresetChanged()
        {
            if (SFile.Preset.SettingsVideo == null)
            {
                return;
            }
            if (SFile.Preset.SettingsAudio == null)
            {
                return;
            }

            SAudio.SampleRate = SFile.Preset.SettingsAudio.SampleRate;
            SAudio.Codec = SFile.Preset.SettingsAudio.Codec;
            SAudio.Bitrate = SFile.Preset.SettingsAudio.Bitrate;

            SVideo.Bitrate = SFile.Preset.SettingsAudio.Bitrate;
            SVideo.PFormat = SFile.Preset.SettingsVideo.PFormat;
            SVideo.Codec = SFile.Preset.SettingsVideo.Codec;
            SVideo.EncodingMode = SFile.Preset.SettingsVideo.EncodingMode;
            SVideo.FrameRate = SFile.Preset.SettingsVideo.FrameRate;
            SVideo.QuailityPreset = SFile.Preset.SettingsVideo.QuailityPreset;
            SVideo.Height = SFile.Preset.SettingsVideo.Height;
            SVideo.Width = SFile.Preset.SettingsVideo.Width;

            SFile.Format = SFile.Preset.Format;
        }

        [RelayCommand]
        public void FormatChanged()
        {
            if (SFile.Preset.Format.Equals(SFile.Format))
            {
                return;
            }
            if (AudioFormats.AvailableAudioFormats.Contains(SFile.Format))
            {
                ConvertVideo = false;
                DisplayVideo = false;
                ConvertAudio = true;
                DisplayAudio = true;
            }
            else
            {
                ConvertVideo = true;
                DisplayVideo = true;
                ConvertAudio = true;
                DisplayAudio = true;
            }
            SFile.Preset = Presets.custom;

            SVideo.Codec = SFile.Format.VideoCodecs.FirstOrDefault();
            SAudio.Codec = SFile.Format.AudioCodecs.FirstOrDefault();
        }

        [RelayCommand]
        public void VideoCodecChanged()
        {
            if (SVideo.Codec.HasVBRSupport)
            {
                if (!encodingProfiles.Contains(EncodingMode.VBR))
                {
                    encodingProfiles.Add(EncodingMode.VBR);
                }
            }
            else
            {
                encodingProfiles.Remove(EncodingMode.VBR);
            }

            if (SFile.Preset.Equals(Presets.custom) || SFile.Preset.Name == "")
            {
                return;
            }
            if (!isInitialisingFile && !SFile.Preset.SettingsVideo.Codec.Equals(SVideo.Codec))
            {
                SFile.Preset = Presets.custom;
            }
        }

        [RelayCommand]
        public void AudioCodecChanged()
        {
            // Only do this for the audio part of a video file
            if (IsAudioFile)
            {
                return;
            }
            if (!isInitialisingFile && !SFile.Preset.SettingsAudio.Codec.Equals(SAudio.Codec))
            {
                SFile.Preset = Presets.custom;
            }
        }

        [RelayCommand]
        public void SampleRateChanged()
        {
            // Only do this for the audio part of a video file
            if (IsAudioFile)
            {
                return;
            }
            if (!isInitialisingFile && SFile.Preset.SettingsAudio.SampleRate != SAudio.SampleRate)
            {
                SFile.Preset = Presets.custom;
            }
        }

        [RelayCommand]
        public void EncodingProfileChanged()
        {
            IsVBRSelected = SVideo.EncodingMode == EncodingMode.VBR;

            if (SFile.Preset.Equals(Presets.custom) || SFile.Preset.Name == "")
            {
                return;
            }
            if (!isInitialisingFile && !SFile.Preset.SettingsVideo.EncodingMode.Equals(SVideo.EncodingMode))
            {
                SFile.Preset = Presets.custom;
            }
        }

        #endregion

        private void UpdateTransferredSettings(object recipient, TransferSettingsMessage message)
        {
            FileItem? fileItem = message.Value;
            isInitialisingFile = true;
            if (fileItem == null)
            {
                SFile = new SettingsFile(string.Empty, string.Empty);
                SMetadata = new SettingsMetadata();
                SVideo = new SettingsVideo();
                SAudio = new SettingsAudio();
                IsAudioFile = false;
                IsFileSelected = false;
                return;
            }
            IsAudioFile = fileItem.IsAudioFile;

            SFile = fileItem.SettingsFile;
            if (!IsAudioFile && SFile.Preset.Name == string.Empty)
            {
                SFile.Preset = PresetList.FirstOrDefault();
            }
            SMetadata = fileItem.SettingsMetadata;
            SVideo = fileItem.SettingsVideo;
            SAudio = fileItem.SettingsAudio;

            IsFileSelected = true;
            if (IsAudioFile)
            {
                ConvertAudio = true;
                DisplayAudio = true;
                ConvertVideo = false;
                DisplayVideo = false;
            }
            else
            {
                ConvertVideo = true;
                DisplayVideo = true;
                ConvertAudio = true;
                DisplayAudio = true;
            }
            isInitialisingFile = false;
        }
    }
}
