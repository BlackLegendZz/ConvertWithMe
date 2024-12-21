namespace ConvertWithMe.Core.Definitions
{
    public static class VideoFormats
    {
        public static Format mp4 = new Format() { 
            VideoCodecs = [VideoCodecs.h264, VideoCodecs.h265, VideoCodecs.mpeg4, VideoCodecs.mpeg2, VideoCodecs.mpeg1], 
            AudioCodecs = [AudioCodecs.mp3, AudioCodecs.aac],
            Extension = "mp4" 
        };

        public static Format wmv = new Format()
        {
            VideoCodecs = [VideoCodecs.wmv],
            AudioCodecs = [AudioCodecs.wmav2],
            Extension = "wmv"
        };

        public static Format flv = new Format()
        {
            VideoCodecs = [VideoCodecs.flv1, VideoCodecs.h264],
            AudioCodecs = [AudioCodecs.mp3, AudioCodecs.aac],
            Extension = "flv"
        };

        public static Format mkv = new Format()
        {
            VideoCodecs = [VideoCodecs.h264, VideoCodecs.h265, VideoCodecs.mpeg4, VideoCodecs.mpeg2, VideoCodecs.mpeg1],
            AudioCodecs = [AudioCodecs.mp3, AudioCodecs.aac, AudioCodecs.flac, AudioCodecs.vorbis],
            Extension = "mkv"
        };

        public static Format m4v = new Format()
        {
            VideoCodecs = [VideoCodecs.h264],
            AudioCodecs = [AudioCodecs.aac],
            Extension = "m4v"
        };

        public static Format[] AvailableVideoFormats = [mp4, wmv, flv, mkv, m4v];
    }
}
