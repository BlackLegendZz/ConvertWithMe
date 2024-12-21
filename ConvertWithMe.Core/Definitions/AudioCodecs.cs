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
            ValidSampleRates = [8000, 11025, 16000, 22050, 32000, 44100, 48000, 88200, 96000, 176400, 192000, 352800, 384000],
            //validSampleFormats = [SampleFormat.s16, SampleFormat.s32]
        };

        public static AudioCodec mp3 = new AudioCodec()
        {
            Name = "MP3",
            Codec = Xabe.FFmpeg.AudioCodec.mp3,
            //hasVBRSupport = true,
            HasVariableSampleRate = false,
            ValidSampleRates = [44100, 48000, 32000, 22050, 24000, 16000, 11025, 12000, 8000],
            //validSampleFormats = [SampleFormat.s32p, SampleFormat.flt, SampleFormat.s16p]
        };

        public static AudioCodec vorbis = new AudioCodec()
        {
            Name = "Vorbis/ogg",
            Codec = Xabe.FFmpeg.AudioCodec.vorbis,
            //hasVBRSupport = false,
            HasVariableSampleRate = true,
            ValidSampleRates = [8000, 11025, 16000, 22050, 32000, 44100, 48000, 88200, 96000, 176400, 192000, 352800, 384000],
            //validSampleFormats = [SampleFormat.fltp]
        };

        public static AudioCodec wmav2 = new AudioCodec()
        {
            Name = "WMA",
            Codec = Xabe.FFmpeg.AudioCodec.wmav2,
            //hasVBRSupport = false,
            HasVariableSampleRate = true,
            ValidSampleRates = [8000, 11025, 16000, 22050, 32000, 44100, 48000, 88200, 96000, 176400, 192000, 352800, 384000],
            //validSampleFormats = [SampleFormat.fltp]
        };

        public static AudioCodec opus = new AudioCodec()
        {
            Name = "Opus",
            Codec = Xabe.FFmpeg.AudioCodec.libopus,
            //hasVBRSupport = true,
            HasVariableSampleRate = false,
            ValidSampleRates = [48000, 24000, 16000, 12000, 8000],
            //validSampleFormats = [SampleFormat.flt, SampleFormat.s16]
        };

        public static AudioCodec aac = new AudioCodec()
        {
            Name = "AAC",
            Codec = Xabe.FFmpeg.AudioCodec.aac,
            //hasVBRSupport = false,
            HasVariableSampleRate = false,
            ValidSampleRates = [96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350],
            //validSampleFormats = [SampleFormat.fltp]
        };
    }
}
