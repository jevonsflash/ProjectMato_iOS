using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public class ArtistInfo
    {
        public string Title
        {
            get;
            set;
        }

        public string GroupHeader { get; set; }

        public ImageSource ArtistArt { get; set; }
        public IEnumerable<MusicInfo> Musics { get; set; }
    }
}
