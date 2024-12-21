namespace ConvertWithMe.Core.Definitions
{
    public readonly struct Format
    {
        public string Extension { get; init; }
        public VideoCodec[] VideoCodecs { get; init; }
        public AudioCodec[] AudioCodecs { get; init; }

    }
}
