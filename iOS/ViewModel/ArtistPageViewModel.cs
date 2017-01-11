using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMato.iOS
{
    public class ArtistPageViewModel : BaseViewModel
    {
        public ArtistPageViewModel(ArtistInfo artistInfo)
        {
            this.ArtistInfo = artistInfo;
        }

        private ArtistInfo _artistInfo;
        public ArtistInfo ArtistInfo
        {
            get { return _artistInfo; }
            set { base.SetObservableProperty(ref _artistInfo, value); }
        }
    }
}
