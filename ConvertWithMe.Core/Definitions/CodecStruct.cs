namespace ConvertWithMe.Core.Definitions
{
    /*
    ffmpeg -hide_banner -sample_fmts
    name   depth
    u8        8
    s16      16
    s32      32
    flt      32
    dbl      64
    u8p       8
    s16p     16
    s32p     32
    fltp     32
    dblp     64
    s64      64
    s64p     64
    */
    public enum SampleFormat {u8, s16, s32, flt, dlb, u8p, s16p, s32p, fltp, dblp, s64, s64p};
    public enum EncodingMode { VBR, CBR };
    public readonly struct VideoCodec
    {
        public string Name { get; init; }
        public Xabe.FFmpeg.VideoCodec Codec { get; init; }
        public bool HasVBRSupport { get; init; }
        public bool HasQualitySupport { get; init; }
        public Xabe.FFmpeg.PixelFormat[] ValidPixelFormats { get; init; }
    }

    public readonly struct AudioCodec
    {
        public string Name { get; init; }
        public Xabe.FFmpeg.AudioCodec Codec { get; init; }
        public int[] ValidSampleRates { get; init; }
        //public SampleFormat[] validSampleFormats { get; init; }
        public bool HasVariableSampleRate { get; init; }
        //public bool hasVBRSupport { get; init; }
        public bool IsLossless { get; init; }
    }
}
