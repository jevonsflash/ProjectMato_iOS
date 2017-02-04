using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistEntryPage : ContentPage
    {

        private PlaylistFunctionPage _editPlaylistFunctionPage;
        private NavigationPage detailPage;

        public PlaylistEntryPage(PlaylistInfo playlist)
        {

            InitializeComponent();
            this.BindingContext = new PlaylistEntryPageViewModel(playlist);
        }

        private async void MusicItemView_OnOnJumptoOtherPage(object sender, MusicFunctionEventArgs e)
        {
            if (e.FunctionType == MusicFunctionType.GoAlbumPage)
            {
                var albumInfo = MusicInfoServer.Current.GetAlbumInfos().Find(c => c.Title == e.MusicInfo.AlbumTitle);
                await Navigation.PushAsync(new AlbumPage(albumInfo));

            }
            else if (e.FunctionType == MusicFunctionType.GoArtistPage)
            {
                var artistInfo = MusicInfoServer.Current.GetArtistInfos().Find(c => c.Title == e.MusicInfo.Artist);
                await Navigation.PushAsync(new ArtistPage(artistInfo));
            }
            else if (e.FunctionType == MusicFunctionType.Delete)
            {
                var playlistEntryViewModel = this.BindingContext as PlaylistEntryPageViewModel;
                if (playlistEntryViewModel != null)
                {
                    playlistEntryViewModel.DeleteAction(e.MusicInfo);
                }
            }
        }

        private void EditButton_OnClicked(object sender, EventArgs e)
        {
            var playlistEntryViewModel = this.BindingContext as PlaylistEntryPageViewModel;
            if (playlistEntryViewModel != null)
            {
                _editPlaylistFunctionPage = new PlaylistFunctionPage(PlaylistFunctionMenuType.Edit, playlistEntryViewModel.Playlist);
                _editPlaylistFunctionPage.OnFinished += _editPlaylistFunctionPage_OnFinished;
                popup.ShowPopup(_editPlaylistFunctionPage);
            }
        }

        private void _editPlaylistFunctionPage_OnFinished(object sender, PlaylistFunctionEventArgs e)
        {
            if (e.FunctionType == PlaylistFunctionType.Submit)
            {
                var playlistEntryViewModel = this.BindingContext as PlaylistEntryPageViewModel;
                if (playlistEntryViewModel != null)
                {
                    playlistEntryViewModel.Playlist = e.PlaylistInfo;
                }
            }
            popup.HidePopup();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            this.detailPage = new NavigationPage(new NowPlayingPage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
            App.MainMasterDetailPage.Detail = this.detailPage;
        }
    }
}
