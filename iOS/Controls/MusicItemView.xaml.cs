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


            var imageButton = sender as ImageButton;
            var musicInfo = imageButton.BindingContext as MusicInfo;
            var _mainMenuCellInfos = new List<MenuCellInfo>()
            {
                new MenuCellInfo() {Title = "删除" ,Code = "Delete",Icon = "Icon/search" },
                new MenuCellInfo() {Title = "添加到.." ,Code = "AddToPlaylist",Icon = "Icon/headphone" },
                new MenuCellInfo() {Title = "下一首播放" ,Code = "NextPlay",Icon = "Icon/queue2" },
                new MenuCellInfo() {Title = "追加到列队" ,Code = "AddToQueue",Icon = "Icon/folder" },
                new MenuCellInfo() {Title = musicInfo.Artist ,Code = "GoArtistPage",Icon = "Icon/playlist" },
                new MenuCellInfo() {Title = musicInfo.AlbumTitle ,Code = "GoAlbumPage",Icon = "Icon/setting" },
            };
            _musicFunctionPage = new MusicFunctionPage(musicInfo, _mainMenuCellInfos);
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
            else if (e.MenuCellInfo.Code == "GoAlbumPage")
            {
                var albumInfo = MusicInfoServer.Current.GetAlbumInfos().Find(c => c.Title == (e.MusicInfo as MusicInfo).AlbumTitle);
                CommonHelper.GoNavigate("ArtistPage", new object[] { albumInfo });
            }
            else if (e.MenuCellInfo.Code == "GoArtistPage")
            {
                var artistInfo = MusicInfoServer.Current.GetArtistInfos().Find(c => c.Title == (e.MusicInfo as MusicInfo).Artist);
                CommonHelper.GoNavigate("ArtistPage", new object[] { artistInfo });
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
