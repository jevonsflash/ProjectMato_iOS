using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.Server;
using Xamarin.Forms;
using XLabs;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS
{
    public partial class LibraryPage
    {
        private PlaylistChoosePage playlistChoosePage;
        private MusicFunctionPage musicFunctionPage;
        private NavigationPage detailPage;

        public LibraryPage()
        {
            InitializeComponent();
            this.BindingContext = new LibraryPageViewModel();

        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is ArtistInfo)
            {
                var artistInfo = e.SelectedItem as ArtistInfo;

                await Navigation.PushAsync(new ArtistPage(artistInfo));

            }
            else if (e.SelectedItem is AlbumInfo)
            {
                var albumInfo = e.SelectedItem as AlbumInfo;

                await Navigation.PushAsync(new AlbumPage(albumInfo));
            }
        }

        private void MusicListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            CommonHelper.GoPage("NowPlayingPage");
        }
    }
}
