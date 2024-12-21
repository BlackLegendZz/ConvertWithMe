namespace ConvertWithMe.Core.Definitions
{
    public static class AudioFormats
    {
        public static Format mp3 = new Format()
        {
            VideoCodecs = [],
            AudioCodecs = [AudioCodecs.mp3],
            Extension = "mp3"
        };

        public static Format aac = new Format()
        {
            VideoCodecs = [],
            AudioCodecs = [AudioCodecs.aac],
            Extension = "aac"
        };

        public static Format flac = new Format()
        {
            VideoCodecs = [],
            AudioCodecs = [AudioCodecs.flac],
            Extension = "flac"
        };

        public static Format m4a = new Format()
        {
            VideoCodecs = [],
            AudioCodecs = [AudioCodecs.aac],
            Extension = "m4a"
        };

        public static Format ogg = new Format()
        {
            VideoCodecs = [],
            AudioCodecs = [AudioCodecs.vorbis, AudioCodecs.opus, AudioCodecs.flac],
            Extension = "ogg"
        };
        public static Format wma = new Format()
        {
            VideoCodecs = [],
            AudioCodecs = [AudioCodecs.wmav2],
            Extension = "wma"
        };

        public static Format[] AvailableAudioFormats = [mp3, aac, flac, m4a, ogg, wma];
    }
}
