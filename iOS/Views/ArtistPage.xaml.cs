using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.Server;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS
{
    public partial class ArtistPage : ContentPage
    {
        private NavigationPage detailPage;

        public ArtistPage(ArtistInfo artistInfo)
        {
            InitializeComponent();
            
            this.BindingContext = new ArtistPageViewModel(artistInfo).ArtistInfo;
            
        }

        private void MusicListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            CommonHelper.GoPage("NowPlayingPage");

        }
    }
}
