using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistPage : ContentPage
    {
        private PlaylistFunctionPage _editPlaylistFunctionPage;
        public PlaylistPage()
        {
            InitializeComponent();
            this.BindingContext = new PlaylistPageViewModel();
        }


        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var playlist = e.SelectedItem as PlaylistInfo;
            CommonHelper.GoNavigate("PlaylistEntryPage", new object[] { playlist });
        }

        private void MusicCollectionItemView_OnOnFinishedChoice(object sender, MusicFunctionEventArgs e)
        {
            var musicCollectionInfo = e.MusicInfo;
            var playlistViewModel = this.BindingContext as PlaylistPageViewModel;

            if (e.MenuCellInfo.Code == "Delete")
            {
                if (playlistViewModel != null)
                    playlistViewModel.DeleteAction(musicCollectionInfo);
            }
            else if (e.MenuCellInfo.Code == "Rename")
            {
                if (playlistViewModel != null)
                {
                    _editPlaylistFunctionPage = new PlaylistFunctionPage(
                        playlistViewModel.Playlists.FirstOrDefault(c => c.Id == (e.MusicInfo as MusicCollectionInfo).Id));
                    _editPlaylistFunctionPage.OnFinished += _editPlaylistFunctionPage_OnFinished;
                    popup3.ShowPopup(_editPlaylistFunctionPage);
                }

            }
        }

        private void _editPlaylistFunctionPage_OnFinished(object sender, CommonFunctionEventArgs e)
        {

            var playlistInfo = e.Info as PlaylistInfo;
            var playlistPageViewModel = this.BindingContext as PlaylistPageViewModel;
            if (playlistPageViewModel != null)
            {
                if (e.Code == "Edit")
                    playlistPageViewModel.EditAction(playlistInfo);
                else
                    playlistPageViewModel.CreateAction(playlistInfo);
            }

            popup3.HidePopup();
        }

        private void CreatePlaylist_OnClicked(object sender, EventArgs e)
        {
            _editPlaylistFunctionPage = new PlaylistFunctionPage(null);
            _editPlaylistFunctionPage.OnFinished += _editPlaylistFunctionPage_OnFinished;
            popup3.ShowPopup(_editPlaylistFunctionPage);
        }
    }
}
