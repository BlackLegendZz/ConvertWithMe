using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertWithMe.UI.Models
{
    public sealed class SettingsFile
    {
        public string Filename { get; set; }
        public string Location { get; set; }


        public SettingsFile(string filename, string location)
        {
            Filename = filename;
            Location = location;
        }
    }
}
