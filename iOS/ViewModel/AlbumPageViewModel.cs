using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMato.iOS
{
   public class AlbumPageViewModel:BaseViewModel
    {
        public AlbumPageViewModel(AlbumInfo albumInfo)
        {
            this.AlbumInfo = albumInfo;
        }

        private AlbumInfo _albumInfo;
        public AlbumInfo AlbumInfo
        {
            get { return _albumInfo; }
            set { base.SetObservableProperty(ref _albumInfo, value); }
        }
    }
}
