using System;
using System.Collections.Generic;
using ProjectMato.iOS.Controls;
using ProjectMato.iOS.Server;
using UIKit;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs.Enums;

namespace ProjectMato.iOS
{
    public partial class NowPlayingPage : ContentPage
    {
        private NavigationPage detailPage;
        private MusicFunctionPage _musicFunctionPage;
        private PlaylistChoosePage _playlistChoosePage;

        public NowPlayingPage()
        {
            InitializeComponent();
            this.BindingContext = MusicRelatedViewModel.Current;
        }



        void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            var musicRelatedViewModel = (this.BindingContext) as MusicRelatedViewModel;
            if (musicRelatedViewModel != null)
                musicRelatedViewModel.ChangeProgess(e.NewValue);
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            this.detailPage = new NavigationPage(new QueuePage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
            ;
            App.MainMasterDetailPage.Detail = this.detailPage;
        }

        private void IsFavouriteButton_OnClicked(object sender, EventArgs e)
        {

            var imageButton = sender as ImageButton;
            var musicInfo = (imageButton.BindingContext as MusicRelatedViewModel).CurrentMusic;
            if (musicInfo != null)
            {
                if (!musicInfo.IsFavourite)
                {

                    if (MusicInfoServer.Current.CreatePlaylistEntryToMyFavourite(musicInfo))
                    {
                        musicInfo.IsFavourite = !musicInfo.IsFavourite;
                    }

                }
                else
                {
                    if (MusicInfoServer.Current.DeletePlaylistEntryFromMyFavourite(musicInfo))
                    {
                        musicInfo.IsFavourite = !musicInfo.IsFavourite;
                    }

                }
            }
        }
        private void MoreButton_OnClicked(object sender, EventArgs e)
        {


            var imageButton = sender as ImageButton;
            var musicInfo = (imageButton.BindingContext as MusicRelatedViewModel).CurrentMusic;

            _musicFunctionPage = new MusicFunctionPage(musicInfo, MusicFunctionMenuType.NowPlaying);
            _musicFunctionPage.OnFinished += MusicFunctionPage_OnFinished;

            this.popup.ShowPopup(_musicFunctionPage);

        }

        private async void MusicFunctionPage_OnFinished(object sender, MusicFunctionEventArgs e)
        {
            //var libraryPageViewModel = this.BindingContext as LibraryPageViewModel;
            if (e.MusicInfo == null)
            {
                return;
            }
            this.popup.HidePopup();
            if (e.FunctionType == MusicFunctionType.AddToPlaylist)
            {
                _playlistChoosePage = new PlaylistChoosePage();
                _playlistChoosePage.OnFinished += (o, c) =>
                {
                    if (c != null)
                    {
                        MusicInfoServer.Current.CreatePlaylistEntry(e.MusicInfo, c.PlaylistId);


                    }
                    this.popup.HidePopup();
                };

                this.popup.ShowPopup(
                    _playlistChoosePage

                    );
            }

            else if (e.FunctionType == MusicFunctionType.GoAlbumPage)
            {
                var albumInfo = MusicInfoServer.Current.GetAlbumInfos().Find(c => c.Title == e.MusicInfo.AlbumTitle);
                await Navigation.PushAsync(new AlbumPage(albumInfo));

            }
            else if (e.FunctionType == MusicFunctionType.GoArtistPage)
            {
                var artistInfo = MusicInfoServer.Current.GetArtistInfos().Find(c => c.Title == e.MusicInfo.Artist);
                await Navigation.PushAsync(new ArtistPage(artistInfo));
            }

        }

    }
}

