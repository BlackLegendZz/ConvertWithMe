namespace ConvertWithMe.Core.Definitions
{
    public static class VideoFormats
    {
        public static Format mp4 = new Format() { 
            videoCodecs = [VideoCodecs.h264, VideoCodecs.h265], 
            audioCodecs = [AudioCodecs.mp3, AudioCodecs.flac], 
            extension = "mp4" 
        };
    }
}
