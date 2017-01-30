using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistPage : ContentPage
    {
        private PlaylistFunctionPage _playlistFunctionPage;

        public PlaylistPage()
        {
            InitializeComponent();
            _playlistFunctionPage = new PlaylistFunctionPage(PlaylistFunctionMenuType.Create);
            _playlistFunctionPage.OnFinished += _playlistFunctionPage_OnFinished;
            this.BindingContext = new PlaylistPageViewModel();
        }

        private void _playlistFunctionPage_OnFinished(object sender, PlaylistFunctionEventArgs e)
        {
            popup3.HidePopup();
        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var playlist = e.SelectedItem as PlaylistInfo;

            await Navigation.PushAsync(new PlaylistEntryPage(playlist));
        }

        private void MusicCollectionItemView_OnOnFinishedChoice(object sender, MusicCollectionFunctionEventArgs e)
        {
            if (e.FunctionType == MusicCollectionFunctionType.Delete)
            {
                var musicCollectionInfo = e.MusicCollectionInfo;
                var playlistViewModel = this.BindingContext as PlaylistPageViewModel;
                if (playlistViewModel != null)
                    playlistViewModel.DeleteAction(musicCollectionInfo);
            }
        }

        private void CreatePlaylist_OnClicked(object sender, EventArgs e)
        {
            popup3.ShowPopup(_playlistFunctionPage);
        }
    }
}
