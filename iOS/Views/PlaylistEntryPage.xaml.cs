using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Helper;
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

            if (e.MenuCellInfo.Code == "Delete")
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
                _editPlaylistFunctionPage = new PlaylistFunctionPage(playlistEntryViewModel.Playlist);
                _editPlaylistFunctionPage.OnFinished += _editPlaylistFunctionPage_OnFinished;
                popup.ShowPopup(_editPlaylistFunctionPage);
            }
        }

        private void _editPlaylistFunctionPage_OnFinished(object sender, CommonFunctionEventArgs e)
        {

            var playlistEntryViewModel = this.BindingContext as PlaylistEntryPageViewModel;
            if (playlistEntryViewModel != null)
            {
                playlistEntryViewModel.Playlist = e.Info as PlaylistInfo;
            }

            popup.HidePopup();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            CommonHelper.GoPage("NowPlayingPage");
        }
    }


}
