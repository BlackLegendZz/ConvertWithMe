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
        private bool displayVideo = true;

        [ObservableProperty]
        private bool convertVideo = true;

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
        private bool isVideoFormatSelected;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AvailableFormats))]
        private bool isAudioFile;

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

            presetManuallyChanged = true;
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
            IsVideoFormatSelected = SFile.Format.VideoCodecs.Length > 0;

            if (presetManuallyChanged)
            {
                presetManuallyChanged = false;
                return;
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
        }

        private void UpdateTransferredSettings(object recipient, TransferSettingsMessage message)
        {
            FileItem? fileItem = message.Value;

            if (fileItem == null)
            {
                SFile = new SettingsFile(string.Empty, string.Empty);
                SMetadata = new SettingsMetadata();
                SVideo = new SettingsVideo();
                SAudio = new SettingsAudio();
                IsAudioFile = false;
                return;
            }
            IsAudioFile = fileItem.IsAudioFile;

            SFile = fileItem.SettingsFile;
            if (!IsAudioFile && SFile.Preset.Name == string.Empty)
            {
                SFile.Preset = PresetList.FirstOrDefault();
            }
            //if (IsAudioFile && SFile.Format.Extension == string.Empty)
            //{
            //    SFile.Format = AvailableFormats.FirstOrDefault();
            //}
            SMetadata = fileItem.SettingsMetadata;
            SVideo = fileItem.SettingsVideo;
            SAudio = fileItem.SettingsAudio;
        }
    }
}
