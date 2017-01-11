using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Server;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class AlbumPage : ContentPage
    {
        private NavigationPage detailPage;

        public AlbumPage(AlbumInfo albumInfo)
        {
            InitializeComponent();
            this.BindingContext = new AlbumPageViewModel(albumInfo).AlbumInfo;
        }
        private void MusicListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            this.detailPage = new NavigationPage(new NowPlayingPage());
            App.MainMasterDetailPage.Detail = this.detailPage;
        }

        private async void MusicItemView_OnOnJumptoOtherPage(object sender, MusicFunctionEventArgs e)
        {
            if (e.FunctionType == FunctionType.GoAlbumPage)
            {
                var albumInfo = MusicInfoServer.Current.GetAlbumInfos().Find(c => c.Title == e.MusicInfo.AlbumTitle);
                await Navigation.PushAsync(new AlbumPage(albumInfo));

            }
            else if (e.FunctionType == FunctionType.GoArtistPage)
            {
                var artistInfo = MusicInfoServer.Current.GetArtistInfos().Find(c => c.Title == e.MusicInfo.Artist);
                await Navigation.PushAsync(new ArtistPage(artistInfo));
            }
        }
    }
}
