using System;
using System.Collections.Generic;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Controls;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;
using ProjectMato.iOS.ViewModel;
using UIKit;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs.Enums;

namespace ProjectMato.iOS
{
    public partial class NowPlayingPage : ContentPage
    {
        private MusicFunctionPage _musicFunctionPage;
        private PlaylistChoosePage _playlistChoosePage;

        public NowPlayingPage()
        {
            InitializeComponent();
            this.BindingContext =new NowPlayingPageViewModel();
        }

        private void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            var bindableObject = sender as BindableObject;
            if (bindableObject != null)
            {
                var musicRelatedViewModel = bindableObject.BindingContext as MusicRelatedViewModel;
                if (musicRelatedViewModel != null)
                    musicRelatedViewModel.ChangeProgess(e.NewValue);
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            CommonHelper.GoPage("QueuePage");
        }

        private void IsFavouriteButton_OnClicked(object sender, EventArgs e)
        {

            var imageButton = sender as BindableObject;
            var musicInfo = (imageButton.BindingContext as MusicRelatedViewModel).CurrentMusic;
            if (musicInfo != null)
            {
                if (!musicInfo.IsFavourite)
                {

                    if (MusicInfoServer.Current.CreatePlaylistEntryToMyFavourite(musicInfo))
                    {
                        musicInfo.IsFavourite = true;
                    }

                }
                else
                {
                    if (MusicInfoServer.Current.DeletePlaylistEntryFromMyFavourite(musicInfo))
                    {
                        musicInfo.IsFavourite = false;
                    }

                }
            }
        }

        private void MoreButton_OnClicked(object sender, EventArgs e)
        {


            var imageButton = sender as BindableObject;
            var musicInfo = (imageButton.BindingContext as MusicRelatedViewModel).CurrentMusic;
            var mainMenuCellInfos = new List<MenuCellInfo>()
            {
                new MenuCellInfo() {Title = "删除", Code = "Delete", Icon = "Icon/remove"},
                new MenuCellInfo() {Title = "添加到..", Code = "AddToPlaylist", Icon = "Icon/addto"},
                new MenuCellInfo() {Title = "下一首播放", Code = "NextPlay", Icon = "Icon/playnext"},
                new MenuCellInfo() {Title = "追加到列队", Code = "AddToQueue", Icon = "Icon/addtostack"},
                new MenuCellInfo()
                {
                    Title = (musicInfo as MusicInfo).Artist,
                    Code = "GoArtistPage",
                    Icon = "Icon/microphone2"
                },
                new MenuCellInfo()
                {
                    Title = (musicInfo as MusicInfo).AlbumTitle,
                    Code = "GoAlbumPage",
                    Icon = "Icon/cd2"
                },


            };
            _musicFunctionPage = new MusicFunctionPage(musicInfo, mainMenuCellInfos);
            _musicFunctionPage.OnFinished += MusicFunctionPage_OnFinished;

            this.popup.ShowPopup(_musicFunctionPage);

        }

        private void MusicFunctionPage_OnFinished(object sender, MusicFunctionEventArgs e)
        {
            //var libraryPageViewModel = this.BindingContext as LibraryPageViewModel;
            if (e.MusicInfo == null)
            {
                return;
            }
            this.popup.HidePopup();
            if (e.MenuCellInfo.Code == "AddToPlaylist")
            {
                _playlistChoosePage = new PlaylistChoosePage();
                _playlistChoosePage.OnFinished += (o, c) =>
                {
                    if (c != null)
                    {
                        MusicInfoServer.Current.CreatePlaylistEntry(e.MusicInfo as MusicInfo, c.PlaylistId);
                    }
                    this.popup.HidePopup();
                };

                this.popup.ShowPopup(
                    _playlistChoosePage

                    );
            }

            else if (e.MenuCellInfo.Code == "GoAlbumPage")
            {
                var albumInfo = MusicInfoServer.Current.GetAlbumInfos().Find(c => c.Title == (e.MusicInfo as MusicInfo).AlbumTitle);
                CommonHelper.GoNavigate("AlbumPage", new object[] { albumInfo });
            }
            else if (e.MenuCellInfo.Code == "GoArtistPage")
            {
                var artistInfo = MusicInfoServer.Current.GetArtistInfos().Find(c => c.Title == (e.MusicInfo as MusicInfo).Artist);
                CommonHelper.GoNavigate("ArtistPage", new object[] { artistInfo });
            }

        }

    }
}

