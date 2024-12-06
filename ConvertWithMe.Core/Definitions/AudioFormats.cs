namespace ConvertWithMe.Core.Definitions
{
    public static class AudioFormats
    {
        public static Format mp3 = new Format()
        {
            videoCodecs = [],
            audioCodecs = [AudioCodecs.mp3],
            extension = "mp3"
        };

        public static Format aac = new Format()
        {
            videoCodecs = [],
            audioCodecs = [AudioCodecs.aac],
            extension = "aac"
        };

        public static Format flac = new Format()
        {
            videoCodecs = [],
            audioCodecs = [AudioCodecs.flac],
            extension = "flac"
        };

        public static Format m4a = new Format()
        {
            videoCodecs = [],
            audioCodecs = [AudioCodecs.aac],
            extension = "m4a"
        };

        public static Format ogg = new Format()
        {
            videoCodecs = [],
            audioCodecs = [AudioCodecs.vorbis, AudioCodecs.opus, AudioCodecs.flac],
            extension = "ogg"
        };


    }
}
