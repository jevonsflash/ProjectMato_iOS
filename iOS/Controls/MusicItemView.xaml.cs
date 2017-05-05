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
using XLabs.Forms.Controls;

namespace ProjectMato.iOS.Controls
{

    public partial class MusicItemView
    {


        private MusicFunctionPage _musicFunctionPage;
        private PlaylistChoosePage _playlistChoosePage;

        public PopupView Popup { get; set; }
        public event EventHandler<MusicFunctionEventArgs> OnFinishedChoice;


        public MusicItemView()
        {
            InitializeComponent();
        }

        public bool IsMusicInfo => this.BindingContext is MusicInfo;

        private void MoreButton_OnClicked(object sender, EventArgs e)
        {

            List<MenuCellInfo> _mainMenuCellInfos;

            if (IsMusicInfo)
            {
                var musicInfo = BindingContext;
                _mainMenuCellInfos = new List<MenuCellInfo>()
                {
                    new MenuCellInfo() {Title = "删除", Code = "Remove", Icon = "Icon/remove"},
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

            }
            else
            {
                _mainMenuCellInfos = new List<MenuCellInfo>()
                {

                    new MenuCellInfo() {Title = "删除", Code = "Delete", Icon = "Icon/remove"},
                    new MenuCellInfo() {Title = "重命名..", Code = "Rename", Icon = "Icon/rename"},
                    new MenuCellInfo() {Title = "播放此专辑", Code = "Play", Icon = "Icon/cdplay"},
                    new MenuCellInfo() {Title = "追加到列队", Code = "AddMusicCollectionToQueue", Icon = "Icon/addtostack"},
                    new MenuCellInfo() {Title = "添加到..", Code = "AddMusicCollectionToPlaylist", Icon = "Icon/addto"},
                    new MenuCellInfo() {Title = "收藏至我最喜爱", Code = "AddToFavourite", Icon = "Icon/favouriteadd"}

                };
            }
            _musicFunctionPage = new MusicFunctionPage(BindingContext as IBasicInfo, _mainMenuCellInfos);
            _musicFunctionPage.OnFinished += MusicFunctionPage_OnFinished;

            this.Popup.ShowPopup(_musicFunctionPage);
        }

        private async void MusicFunctionPage_OnFinished(object sender, MusicFunctionEventArgs e)
        {
            if (e.MusicInfo == null)
            {
                return;
            }
            this.Popup.HidePopup();
            if (e.MenuCellInfo.Code == "AddToPlaylist")
            {
                _playlistChoosePage = new PlaylistChoosePage();
                _playlistChoosePage.OnFinished += (o, c) =>
                {
                    if (c != null)
                    {
                        MusicInfoServer.Current.CreatePlaylistEntry((e.MusicInfo as MusicInfo), c.PlaylistId);


                    }
                    this.Popup.HidePopup();
                };

                this.Popup.ShowPopup(
                    _playlistChoosePage

                    );
            }
            else if (e.MenuCellInfo.Code == "NextPlay")
            {
                MusicInfoServer.Current.InsertToNextQueueEntry(e.MusicInfo as MusicInfo);

            }
            else if (e.MenuCellInfo.Code == "AddToQueue")
            {
                MusicInfoServer.Current.InsertToEndQueueEntry(e.MusicInfo as MusicInfo);
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
            else if (e.MenuCellInfo.Code == "AddMusicCollectionToPlaylist")
            {

                _playlistChoosePage = new PlaylistChoosePage();
                _playlistChoosePage.OnFinished += (o, c) =>
                {
                    if (c != null)
                    {
                        MusicInfoServer.Current.CreatePlaylistEntrys(e.MusicInfo as MusicCollectionInfo, c.PlaylistId);
                    }
                    this.Popup.HidePopup();
                };

                this.Popup.ShowPopup(
                   _playlistChoosePage

                   );
            }
            else if (e.MenuCellInfo.Code == "AddMusicCollectionToQueue")
            {
                var musicCollectionInfo = e.MusicInfo as MusicCollectionInfo;
                if (musicCollectionInfo != null)
                    MusicInfoServer.Current.InsertToEndQueueEntrys(musicCollectionInfo.Musics
                        .ToList());
            }
            OnFinishedChoice?.Invoke(sender, e);

        }
    }
}
