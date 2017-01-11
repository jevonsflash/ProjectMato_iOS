using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public class AlbumInfo
    {
        public string Title
        {
            get;
            set;
        }

        public string Artist
        {
            get;
            set;
        }
        public ImageSource AlbumArt { get; set; }
        public IEnumerable<MusicInfo> Musics { get; set; }
        public string GroupHeader { get; set; }
    }
}
