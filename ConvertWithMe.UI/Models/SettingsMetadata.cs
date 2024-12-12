using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertWithMe.UI.Models
{
    public sealed class SettingsMetadata : ISettings
    {
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string AlbumArtist { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public uint Year { get; set; } = 0;
        public uint Tracknumber { get; set; } = 0;

        public SettingsMetadata()
        {
            
        }

        public SettingsMetadata(string title, string artist, string album, string albumArtist, string genre,uint year, uint trackNumber)
        {
            Title = title;
            Artist = artist;
            Album = album;
            AlbumArtist = albumArtist;
            Genre = genre;
            Year = year;
            Tracknumber = trackNumber;
        }
    }
}
