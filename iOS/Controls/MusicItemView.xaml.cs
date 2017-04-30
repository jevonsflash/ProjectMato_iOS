using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void MoreButton_OnClicked(object sender, EventArgs e)
        {

            List<MenuCellInfo> _mainMenuCellInfos;
            var imageButton = sender as ImageButton;
            var IsMusicInfo = imageButton.BindingContext is MusicInfo;
            if (IsMusicInfo)
            {
                var musicInfo = imageButton.BindingContext;
                _mainMenuCellInfos = new List<MenuCellInfo>()
                {
                    new MenuCellInfo() {Title = "删除", Code = "Delete", Icon = "Icon/search"},
                    new MenuCellInfo() {Title = "添加到..", Code = "AddToPlaylist", Icon = "Icon/headphone"},
                    new MenuCellInfo() {Title = "下一首播放", Code = "NextPlay", Icon = "Icon/queue2"},
                    new MenuCellInfo() {Title = "追加到列队", Code = "AddToQueue", Icon = "Icon/folder"},
                    new MenuCellInfo()
                    {
                        Title = (musicInfo as MusicInfo).Artist,
                        Code = "GoArtistPage",
                        Icon = "Icon/playlist"
                    },
                    new MenuCellInfo() {Title = (musicInfo as MusicInfo).AlbumTitle, Code = "GoAlbumPage", Icon = "Icon/setting"},


                };

            }
            else
            {
                _mainMenuCellInfos = new List<MenuCellInfo>()
                {

                    new MenuCellInfo() {Title = "删除", Code = "Delete", Icon = "Icon/search"},
                    new MenuCellInfo() {Title = "重命名..", Code = "Rename", Icon = "Icon/headphone"},
                    new MenuCellInfo() {Title = "播放此专辑", Code = "Play", Icon = "Icon/queue2"},
                    new MenuCellInfo() {Title = "追加到列队", Code = "AddMusicCollectionToQueue", Icon = "Icon/folder"},
                    new MenuCellInfo() {Title = "添加到..", Code = "AddMusicCollectionToPlaylist", Icon = "Icon/playlist"},
                    new MenuCellInfo() {Title = "收藏至我最喜爱", Code = "AddToFavourite", Icon = "Icon/setting"}

                };
            }
            _musicFunctionPage = new MusicFunctionPage(imageButton.BindingContext as IBasicInfo, _mainMenuCellInfos);
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
                //todo:尚未實現

            }

            else if (e.MenuCellInfo.Code == "AddToQueue")
            {
                MusicInfoServer.Current.CreateQueueEntry(e.MusicInfo as MusicInfo);
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
            if (e.MenuCellInfo.Code == "AddMusicCollectionToPlaylist")
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
            OnFinishedChoice?.Invoke(sender, e);

        }
        private void IsFavouriteButton_OnClicked(object sender, EventArgs e)
        {

            var imageButton = sender as ImageButton;
            var musicInfo = imageButton.BindingContext as MusicInfo;
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
    }
}
