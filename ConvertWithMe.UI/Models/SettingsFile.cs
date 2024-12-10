using System.IO;

namespace ConvertWithMe.UI.Models
{
    public sealed class SettingsFile
    {
        public string FilenameSrc {  get; init; }
        public string DirSrc { get; init; }
        public string FilenameDest { get; set; }
        public string DirDest { get; set; }


        public SettingsFile(string src, string dest)
        {
            FilenameSrc = Path.GetFileName(src);
            FilenameDest = Path.GetFileName(dest);

            DirSrc = Path.GetDirectoryName(src) ?? string.Empty;
            DirDest = Path.GetDirectoryName(dest) ?? string.Empty;
        }
    }
}
