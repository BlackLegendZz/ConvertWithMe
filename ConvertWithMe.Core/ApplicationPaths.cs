using System.IO;

namespace ConvertWithMe.Core
{
    public static class ApplicationPaths
    {
        private static readonly string mainPath = "ConvertWithMe";

        public static readonly string Main = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), mainPath);
        public static readonly string Thumbnails = Path.Combine(Main, "Thumbnails");
        public static readonly string Logs = Path.Combine(Main, "Logs");
        public static readonly string FFmpeg = Path.Combine(Main, "ffmpeg");
    }
}
