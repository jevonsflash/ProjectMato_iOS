using System;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public class MusicInfo : ViewModelBase, IBasicInfo
    {
        public MusicInfo()
        {

        }

        public int Id { get; set; }
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
        public bool IsFavourite
        {
            get { return isFavourite; }

            set
            {
                isFavourite = value;
                base.RaisePropertyChanged();
            }
        }
        public string GroupHeader { get; set; }
        public ImageSource AlbumArt { get; set; }
        /// <summary>
        /// 提供搜索线索
        /// </summary>
        /// <returns>搜索线索字符串</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Title, Artist, AlbumTitle);
        }
    }
}


