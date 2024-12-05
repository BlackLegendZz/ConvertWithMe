namespace ConvertWithMe.Core.Definitions
{
    public static class VideoFormats
    {
        public static Format mp4 = new Format() { 
            videoCodecs = [VideoCodecs.h264, VideoCodecs.h265, VideoCodecs.mpeg4, VideoCodecs.mpeg2, VideoCodecs.mpeg1], 
            audioCodecs = [AudioCodecs.mp3, AudioCodecs.aac],
            extension = "mp4" 
        };

        public static Format wmv = new Format()
        {
            videoCodecs = [VideoCodecs.wmv],
            audioCodecs = [AudioCodecs.wmav2],
            extension = "wmv"
        };

        public static Format flv = new Format()
        {
            videoCodecs = [VideoCodecs.flv1, VideoCodecs.h264],
            audioCodecs = [AudioCodecs.mp3, AudioCodecs.aac],
            extension = "flv"
        };

        public static Format mkv = new Format()
        {
            videoCodecs = [VideoCodecs.h264, VideoCodecs.h265, VideoCodecs.mpeg4, VideoCodecs.mpeg2, VideoCodecs.mpeg1],
            audioCodecs = [AudioCodecs.mp3, AudioCodecs.aac, AudioCodecs.flac, AudioCodecs.vorbis],
            extension = "mkv"
        };

        public static Format m4v = new Format()
        {
            videoCodecs = [VideoCodecs.h264],
            audioCodecs = [AudioCodecs.aac],
            extension = "m4v"
        };
    }
}
