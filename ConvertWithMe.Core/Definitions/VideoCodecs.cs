using Xabe.FFmpeg;

namespace ConvertWithMe.Core.Definitions
{
    public static class VideoCodecs
    {
        public static VideoCodec mpeg1 = new VideoCodec() 
        { 
            Name = "MPEG1",
            Codec = Xabe.FFmpeg.VideoCodec.mpeg1video, 
            HasVBRSupport = false,
            HasQualitySupport = false,
            ValidPixelFormats = [PixelFormat.yuv420p]
        };

        public static VideoCodec mpeg2 = new VideoCodec()
        {
            Name = "MPEG2",
            Codec = Xabe.FFmpeg.VideoCodec.mpeg2video,
            HasVBRSupport = false,
            HasQualitySupport = false,
            ValidPixelFormats = [PixelFormat.yuv420p, PixelFormat.yuv422p]
        };

        public static VideoCodec mpeg4 = new VideoCodec()
        {
            Name = "MPEG4",
            Codec = Xabe.FFmpeg.VideoCodec.mpeg4,
            HasVBRSupport = false,
            HasQualitySupport = false,
            ValidPixelFormats = [PixelFormat.yuv420p]
        };

        public static VideoCodec h264 = new VideoCodec()
        {
            Name = "H.264",
            Codec = Xabe.FFmpeg.VideoCodec.libx264,
            HasVBRSupport = true,
            HasQualitySupport = true,
            ValidPixelFormats = [
                PixelFormat.yuv420p, PixelFormat.yuv422p, PixelFormat.yuv444p,              // limited range
                PixelFormat.yuvj420p, PixelFormat.yuvj422p, PixelFormat.yuvj444p,           // full range
                PixelFormat.yuv420p10le, PixelFormat.yuv422p10le, PixelFormat.yuv444p10le   // hdr
                ]
        };

        public static VideoCodec h265 = new VideoCodec()
        {
            Name = "H.265",
            Codec = Xabe.FFmpeg.VideoCodec.hevc,
            HasVBRSupport = true,
            HasQualitySupport = true,
            ValidPixelFormats = [
                PixelFormat.yuv420p, PixelFormat.yuv422p, PixelFormat.yuv444p,              // limited range
                PixelFormat.yuvj420p, PixelFormat.yuvj422p, PixelFormat.yuvj444p,           // full range
                PixelFormat.yuv420p10le, PixelFormat.yuv422p10le, PixelFormat.yuv444p10le,  // hdr
                PixelFormat.gbrp, PixelFormat.gbrp10le, PixelFormat.gbrp12le                // RGB
                ]
        };

        public static VideoCodec flv1 = new VideoCodec()
        {
            Name = "FLV",
            Codec = Xabe.FFmpeg.VideoCodec.flv1,
            HasVBRSupport = false,
            HasQualitySupport = false,
            ValidPixelFormats = [PixelFormat.yuv420p]
        };

        public static VideoCodec wmv = new VideoCodec()
        {
            Name = "WMV",
            Codec = Xabe.FFmpeg.VideoCodec.wmv2,
            HasVBRSupport = false,
            HasQualitySupport = false,
            ValidPixelFormats = [PixelFormat.yuv420p]
        };
    }
}
