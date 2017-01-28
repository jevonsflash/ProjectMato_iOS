using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public class AlbumInfo : MusicCollectionInfo
    {
        public string Artist
        {
            get;
            set;
        }
        public ImageSource AlbumArt { get; set; }
    }
}
