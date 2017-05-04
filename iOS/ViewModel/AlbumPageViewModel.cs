using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace ProjectMato.iOS
{
   public class AlbumPageViewModel:ViewModelBase
    {
        public AlbumPageViewModel(AlbumInfo albumInfo)
        {
            this.AlbumInfo = albumInfo;
        }

        private AlbumInfo _albumInfo;
        public AlbumInfo AlbumInfo
        {
            get { return _albumInfo; }
            set
            {
                _albumInfo = value;
                base.RaisePropertyChanged();
            }
        }
    }
}
