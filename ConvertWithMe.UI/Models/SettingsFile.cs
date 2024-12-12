using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;

namespace ConvertWithMe.UI.Models
{
    public partial class SettingsFile : ObservableObject, ISettings
    {
        public string FilenameSrc {  get; init; }
        public string DirSrc { get; init; }

        [ObservableProperty]
        private string filenameDest = string.Empty;

        [ObservableProperty]
        private string dirDest = string.Empty;


        public SettingsFile(string src, string dest)
        {
            FilenameSrc = Path.GetFileName(src);
            FilenameDest = Path.GetFileName(dest);

            DirSrc = Path.GetDirectoryName(src) ?? string.Empty;
            DirDest = Path.GetDirectoryName(dest) ?? string.Empty;
        }
    }
}
