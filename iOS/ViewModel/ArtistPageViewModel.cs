using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace ProjectMato.iOS
{
    public class ArtistPageViewModel : ViewModelBase
    {
        public ArtistPageViewModel(ArtistInfo artistInfo)
        {
            this.ArtistInfo = artistInfo;
        }

        private ArtistInfo _artistInfo;
        public ArtistInfo ArtistInfo
        {
            get { return _artistInfo; }
            set
            {
                _artistInfo = value;
                base.RaisePropertyChanged();
            }
        }
    }
}
