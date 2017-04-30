using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Server;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class SearchPage : ContentPage
    {
        private NavigationPage detailPage;

        public SearchPage()
        {
            InitializeComponent();
            this.BindingContext = new SearchPageViewModel();
        }

        private async void MusicItemView_OnOnFinishedChoice(object sender, MusicFunctionEventArgs e)
        {
            //if (e.FunctionType == MusicFunctionType.GoAlbumPage)
            //{
            //    var albumInfo = MusicInfoServer.Current.GetAlbumInfos().Find(c => c.Title == e.MusicInfo.AlbumTitle);
            //    await Navigation.PushAsync(new AlbumPage(albumInfo));

            //}
            //else if (e.FunctionType == MusicFunctionType.GoArtistPage)
            //{
            //    var artistInfo = MusicInfoServer.Current.GetArtistInfos().Find(c => c.Title == e.MusicInfo.Artist);
            //    await Navigation.PushAsync(new ArtistPage(artistInfo));
            //}

        }

        private void AutoCompleteView_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            this.detailPage = new NavigationPage(new NowPlayingPage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
            App.MainMasterDetailPage.Detail = this.detailPage;
        }
    }
}
