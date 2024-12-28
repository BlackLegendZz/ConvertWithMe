namespace ConvertWithMe.Core.Definitions
{
    public static class AudioCodecs
    {
        public static AudioCodec flac = new AudioCodec()
        {
            Name = "FLAC",
            Codec = Xabe.FFmpeg.AudioCodec.flac,
            //hasVBRSupport = false,
            HasVariableSampleRate = true,
            ValidSampleRates = [384000, 352800, 192000, 176400, 96000, 88200, 48000, 44100, 32000, 22050, 16000, 11025, 8000],
            //validSampleFormats = [SampleFormat.s16, SampleFormat.s32],
            IsLossless = true,
        };

        public static AudioCodec mp3 = new AudioCodec()
        {
            Name = "MP3",
            Codec = Xabe.FFmpeg.AudioCodec.mp3,
            //hasVBRSupport = true,
            HasVariableSampleRate = false,
            ValidSampleRates = [48000, 44100, 32000, 22050, 24000, 16000, 11025, 12000, 8000],
            //validSampleFormats = [SampleFormat.s32p, SampleFormat.flt, SampleFormat.s16p],
            IsLossless = false,
        };

        public static AudioCodec vorbis = new AudioCodec()
        {
            Name = "Vorbis/ogg",
            Codec = Xabe.FFmpeg.AudioCodec.vorbis,
            //hasVBRSupport = false,
            HasVariableSampleRate = true,
            ValidSampleRates = [384000, 352800, 192000, 176400, 96000, 88200, 48000, 44100, 32000, 22050, 16000, 11025, 8000],
            //validSampleFormats = [SampleFormat.fltp],
            IsLossless = false,
        };

        public static AudioCodec wmav2 = new AudioCodec()
        {
            Name = "WMA",
            Codec = Xabe.FFmpeg.AudioCodec.wmav2,
            //hasVBRSupport = false,
            HasVariableSampleRate = true,
            ValidSampleRates = [384000, 352800, 192000, 176400, 96000, 88200, 48000, 44100, 32000, 22050, 16000, 11025, 8000],
            //validSampleFormats = [SampleFormat.fltp],
            IsLossless = false,
        };

        public static AudioCodec opus = new AudioCodec()
        {
            Name = "Opus",
            Codec = Xabe.FFmpeg.AudioCodec.libopus,
            //hasVBRSupport = true,
            HasVariableSampleRate = false,
            ValidSampleRates = [48000, 24000, 16000, 12000, 8000],
            //validSampleFormats = [SampleFormat.flt, SampleFormat.s16],
            IsLossless = false,
        };

        public static AudioCodec aac = new AudioCodec()
        {
            Name = "AAC",
            Codec = Xabe.FFmpeg.AudioCodec.aac,
            //hasVBRSupport = false,
            HasVariableSampleRate = false,
            ValidSampleRates = [96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350],
            //validSampleFormats = [SampleFormat.fltp],
            IsLossless = false,
        };
    }
}
