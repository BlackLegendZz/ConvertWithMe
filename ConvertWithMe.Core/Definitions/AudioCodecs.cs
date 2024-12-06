namespace ConvertWithMe.Core.Definitions
{
    public static class AudioCodecs
    {
        public static AudioCodec flac = new AudioCodec()
        {
            codec = Xabe.FFmpeg.AudioCodec.flac,
            //hasVBRSupport = false,
            hasVariableSampleRate = true,
            validSampleRates = [8000, 11025, 16000, 22050, 32000, 44100, 48000, 88200, 96000, 176400, 192000, 352800, 384000],
            //validSampleFormats = [SampleFormat.s16, SampleFormat.s32]
        };

        public static AudioCodec mp3 = new AudioCodec()
        {
            codec = Xabe.FFmpeg.AudioCodec.mp3,
            //hasVBRSupport = true,
            hasVariableSampleRate = false,
            validSampleRates = [44100, 48000, 32000, 22050, 24000, 16000, 11025, 12000, 8000],
            //validSampleFormats = [SampleFormat.s32p, SampleFormat.flt, SampleFormat.s16p]
        };

        public static AudioCodec vorbis = new AudioCodec()
        {
            codec = Xabe.FFmpeg.AudioCodec.vorbis,
            //hasVBRSupport = false,
            hasVariableSampleRate = true,
            validSampleRates = [8000, 11025, 16000, 22050, 32000, 44100, 48000, 88200, 96000, 176400, 192000, 352800, 384000],
            //validSampleFormats = [SampleFormat.fltp]
        };

        public static AudioCodec wmav2 = new AudioCodec()
        {
            codec = Xabe.FFmpeg.AudioCodec.wmav2,
            //hasVBRSupport = false,
            hasVariableSampleRate = true,
            validSampleRates = [8000, 11025, 16000, 22050, 32000, 44100, 48000, 88200, 96000, 176400, 192000, 352800, 384000],
            //validSampleFormats = [SampleFormat.fltp]
        };

        public static AudioCodec opus = new AudioCodec()
        {
            codec = Xabe.FFmpeg.AudioCodec.libopus,
            //hasVBRSupport = true,
            hasVariableSampleRate = false,
            validSampleRates = [48000, 24000, 16000, 12000, 8000],
            //validSampleFormats = [SampleFormat.flt, SampleFormat.s16]
        };

        public static AudioCodec aac = new AudioCodec()
        {
            codec = Xabe.FFmpeg.AudioCodec.aac,
            //hasVBRSupport = false,
            hasVariableSampleRate = false,
            validSampleRates = [96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350],
            //validSampleFormats = [SampleFormat.fltp]
        };
    }
}
