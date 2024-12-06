using Xabe.FFmpeg;

namespace ConvertWithMe.Core.Definitions
{
    public static class VideoCodecs
    {
        public static VideoCodec mpeg1 = new VideoCodec() 
        { 
            codec = Xabe.FFmpeg.VideoCodec.mpeg1video, 
            hasVBRSupport = false,
            hasQualitySupport = false,
            validPixelFormats = [PixelFormat.yuv420p]
        };

        public static VideoCodec mpeg2 = new VideoCodec()
        {
            codec = Xabe.FFmpeg.VideoCodec.mpeg2video,
            hasVBRSupport = false,
            hasQualitySupport = false,
            validPixelFormats = [PixelFormat.yuv420p, PixelFormat.yuv422p]
        };

        public static VideoCodec mpeg4 = new VideoCodec()
        {
            codec = Xabe.FFmpeg.VideoCodec.mpeg4,
            hasVBRSupport = false,
            hasQualitySupport = false,
            validPixelFormats = [PixelFormat.yuv420p]
        };

        public static VideoCodec h264 = new VideoCodec()
        {
            codec = Xabe.FFmpeg.VideoCodec.libx264,
            hasVBRSupport = true,
            hasQualitySupport = true,
            validPixelFormats = [
                PixelFormat.yuv420p, PixelFormat.yuv422p, PixelFormat.yuv444p,              // limited range
                PixelFormat.yuvj420p, PixelFormat.yuvj422p, PixelFormat.yuvj444p,           // full range
                PixelFormat.yuv420p10le, PixelFormat.yuv422p10le, PixelFormat.yuv444p10le   // hdr
                ]
        };

        public static VideoCodec h265 = new VideoCodec()
        {
            codec = Xabe.FFmpeg.VideoCodec.hevc,
            hasVBRSupport = true,
            hasQualitySupport = true,
            validPixelFormats = [
                PixelFormat.yuv420p, PixelFormat.yuv422p, PixelFormat.yuv444p,              // limited range
                PixelFormat.yuvj420p, PixelFormat.yuvj422p, PixelFormat.yuvj444p,           // full range
                PixelFormat.yuv420p10le, PixelFormat.yuv422p10le, PixelFormat.yuv444p10le,  // hdr
                PixelFormat.gbrp, PixelFormat.gbrp10le, PixelFormat.gbrp12le                // RGB
                ]
        };

        public static VideoCodec flv1 = new VideoCodec()
        {
            codec = Xabe.FFmpeg.VideoCodec.flv1,
            hasVBRSupport = false,
            hasQualitySupport = false,
            validPixelFormats = [PixelFormat.yuv420p]
        };

        public static VideoCodec wmv = new VideoCodec()
        {
            codec = Xabe.FFmpeg.VideoCodec.wmv2,
            hasVBRSupport = false,
            hasQualitySupport = false,
            validPixelFormats = [PixelFormat.yuv420p]
        };
    }
}
