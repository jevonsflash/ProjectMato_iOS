using System;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public class MusicInfo:BaseViewModel
    {
        public MusicInfo()
        {

        }

        public string Id { get; set; }
        public string Title
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        public string AlbumTitle
        {
            get;
            set;
        }
        public string Artist
        {
            get;
            set;
        }

        public bool isFavourite;
        public bool IsFavourite {
            get { return isFavourite; }

            set { base.SetObservableProperty(ref isFavourite,value);}
        }
        public string GroupHeader { get; set; }
        public ImageSource AlbumArt { get; set; }
    }
}

