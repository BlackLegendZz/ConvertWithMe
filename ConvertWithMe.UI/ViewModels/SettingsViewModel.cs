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
        #region VARS

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
        [NotifyPropertyChangedFor(nameof(CanConvertVideo))]
        private bool isAudioFile;

        public bool CanConvertVideo => !(IsAudioFile || AudioFormats.AvailableAudioFormats.Contains(SFile.Format));

        [ObservableProperty]
        private bool isVBRSelected = false;

        [ObservableProperty]
        private bool isFileSelected = false;

        [ObservableProperty]
        private string qualityHint = "";

        private ObservableCollection<EncodingMode> encodingProfiles;
        public IEnumerable<EncodingMode> EncodingProfiles => encodingProfiles;
        private ObservableCollection<Preset> presetList;
        public IEnumerable<Preset> PresetList => presetList;
        private float[] initialFramrates = [24, 25, 30, 60];
        private ObservableCollection<float> framerates;
        public IEnumerable<float> Framrates => framerates;

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
        public IEnumerable<ConversionPreset> QualityPresets => Enum.GetValues(typeof(ConversionPreset)).Cast<ConversionPreset>();
        private bool isInitialisingFile = false;

        #endregion

        public SettingsViewModel()
		{

            SFile = new SettingsFile(string.Empty, string.Empty);
            SMetadata = new SettingsMetadata();
            SVideo = new SettingsVideo();
            SAudio = new SettingsAudio();

            encodingProfiles = new ObservableCollection<EncodingMode>(Enum.GetValues(typeof(EncodingMode)).Cast<EncodingMode>());
            framerates = new ObservableCollection<float>(initialFramrates);
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
            if (SFile.Preset.SettingsVideo == null && SFile.Preset.SettingsAudio == null)
            {
                return;
            }

            if (SFile.Preset.SettingsAudio != null)
            {
                SAudio.SampleRate = SFile.Preset.SettingsAudio.SampleRate;
                SAudio.Codec = SFile.Preset.SettingsAudio.Codec;
                SAudio.Bitrate = SFile.Preset.SettingsAudio.Bitrate;
            }

            if (SFile.Preset.SettingsVideo != null)
            {
                SVideo.Bitrate = SFile.Preset.SettingsVideo.Bitrate;
                SVideo.PFormat = SFile.Preset.SettingsVideo.PFormat;
                SVideo.Codec = SFile.Preset.SettingsVideo.Codec;
                SVideo.EncodingMode = SFile.Preset.SettingsVideo.EncodingMode;
                // Override the original Framerate. Otherwise use the native framrate
                if (SFile.Preset.SettingsVideo.FrameRate != -1)
                {
                    SVideo.FrameRate = SFile.Preset.SettingsVideo.FrameRate;
                }
                SVideo.QuailityPreset = SFile.Preset.SettingsVideo.QuailityPreset;

                // Override the original height. Otherwise use the native height
                if (SFile.Preset.SettingsVideo.Height != -1)
                {
                    SVideo.Height = SFile.Preset.SettingsVideo.Height;
                }
                // Override the original width. Otherwise use the native with
                if (SFile.Preset.SettingsVideo.Width != -1)
                {
                    SVideo.Width = SFile.Preset.SettingsVideo.Width;
                }
            }
            SFile.Format = SFile.Preset.Format;
        }

        [RelayCommand]
        public void FormatChanged()
        {
            OnPropertyChanged(nameof(CanConvertVideo)); // Manually tell UI that the variable updated
            if (!CanConvertVideo)
            {
                ConvertVideo = false;
                DisplayVideo = false;
                ConvertAudio = true;
                DisplayAudio = true;
            }
            else
            {
                if (SFile.Preset.Format.Equals(SFile.Format))
                {
                    return;
                }
                ConvertVideo = true;
                DisplayVideo = true;
                ConvertAudio = true;
                DisplayAudio = true;

                // The format has been changed and is not part of the preset so the user currently works with custom settings.
                SFile.Preset = Presets.custom;
                SVideo.Codec = SFile.Format.VideoCodecs.FirstOrDefault();
            }

            SAudio.Codec = SFile.Format.AudioCodecs.FirstOrDefault();
            if (SAudio.Codec.ValidSampleRates.Contains(48000))
            {
                SAudio.SampleRate = 48000;
            }
            else
            {
                SAudio.SampleRate = SAudio.Codec.ValidSampleRates.FirstOrDefault();
            }
            SAudio.Bitrate = 320;
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
            // Set the Sample Rate to 48k (as it is the normally used one)
            // or the highest one available, in case the currently selected
            // Sample Rate is notsupported by the codec.
            if (!SAudio.Codec.ValidSampleRates.Contains(SAudio.SampleRate))
            {
                if (SAudio.Codec.ValidSampleRates.Contains(48000))
                {
                    SAudio.SampleRate = 48000;
                }
                else
                {
                    SAudio.SampleRate = SAudio.Codec.ValidSampleRates.FirstOrDefault();
                }
                
            }
            // Only do this for the audio part of a video file
            if (IsAudioFile)
            {
                return;
            }

            if (SFile.Preset.Equals(Presets.custom) || SFile.Preset.Name == "")
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

            if (SFile.Preset.Equals(Presets.custom) || SFile.Preset.Name == "")
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

        #region Slider Triggers

        [RelayCommand]
        public void QualitySliderChanged()
        {
            if (SVideo.Bitrate == 0)
            {
                QualityHint = "(Lossless)";
                return;
            }
            if (SVideo.Bitrate > 0 && SVideo.Bitrate <= 10)
            {
                QualityHint = "(Quasi lossless)";
                return;
            }
            if (SVideo.Bitrate > 10 && SVideo.Bitrate <= 15)
            {
                QualityHint = "(Very high quality)";
                return;
            }
            if (SVideo.Bitrate > 15 && SVideo.Bitrate <= 20)
            {
                QualityHint = "(High quality)";
                return;
            }
            if (SVideo.Bitrate > 20 && SVideo.Bitrate <= 25)
            {
                QualityHint = "(Medium quality)";
                return;
            }
            if (SVideo.Bitrate > 25 && SVideo.Bitrate <= 30)
            {
                QualityHint = "(Low quality)";
                return;
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

            if (!IsAudioFile && fileItem.SettingsFile.Preset.Name == string.Empty)
            {
                fileItem.SettingsFile.Preset = PresetList.FirstOrDefault();
            }
            if (IsAudioFile && fileItem.SettingsFile.Format.Extension == string.Empty)
            {
                fileItem.SettingsFile.Format = AvailableFormats.FirstOrDefault();
            }

            SMetadata = fileItem.SettingsMetadata;
            SVideo = fileItem.SettingsVideo;
            SAudio = fileItem.SettingsAudio;
            SFile = fileItem.SettingsFile;

            // Update the preset of the video file manually, in case its similar to the 
            // preset of the currently selected file, since that doesnt trigger the function
            if (!IsAudioFile && fileItem.SettingsFile.Preset.Equals(SFile.Preset))
            {
                PresetChanged();
            }

            // Same thing for the format but this time its exclusive to the audio file
            // as for video files the PresetChanged() function already sets every field
            if (IsAudioFile && fileItem.SettingsFile.Format.Equals(SFile.Format))
            {
                FormatChanged();
            }

            IsFileSelected = true;
            if (!CanConvertVideo)
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

            // Update the initial framrate of the video
            if (!IsAudioFile && fileItem.PrimaryVideoStream != null)
            {
                float fr = (float)fileItem.PrimaryVideoStream.Framerate;
                if (!initialFramrates.Contains(fr))
                {
                    // Update the first entry of the list (aka the framerate of the video)
                    // or add it if it doesnt exist
                    if (framerates.Count > initialFramrates.Length)
                    {
                        framerates[0] = fr;
                    }
                    else
                    {
                        framerates.Insert(0, fr);
                    }
                }
                else
                {
                    if (framerates.Count > initialFramrates.Length)
                    {
                        framerates.RemoveAt(0);
                    }
                }
            }

            isInitialisingFile = false;
        }
    }
}
