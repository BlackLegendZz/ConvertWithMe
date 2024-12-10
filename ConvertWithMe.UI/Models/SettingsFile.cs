using System.IO;

namespace ConvertWithMe.UI.Models
{
    public sealed class SettingsFile
    {
        public FileInfo FileSrc {  get; set; }
        public FileInfo FileDest { get; set; }


        public SettingsFile(string src, string dest)
        {
            FileSrc = new FileInfo(src);
            FileDest = new FileInfo(dest);
        }

        public SettingsFile(FileInfo src, FileInfo dest)
        {
            FileSrc = src;
            FileDest = dest;
        }
    }
}
