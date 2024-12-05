namespace ConvertWithMe.Core.Definitions
{
    public readonly struct Format
    {
        public string extension { get; init; }
        public VideoCodec[]? videoCodecs { get; init; }
        public AudioCodec[] audioCodecs { get; init; }

    }
}
