using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Server;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS.Controls
{
    public partial class MusicItemView
    {


        private MusicFunctionPage _musicFunctionPage;
        private PlaylistChoosePage _playlistChoosePage;

        public PopupLayout Popup { get; set; }
        public FunctionMenuType MenuType { get; set; }
        public event EventHandler<MusicFunctionEventArgs> OnJumptoOtherPage;

        public MusicItemView(FunctionMenuType menuType) : this()
        {

            this.MenuType = menuType;
        }

        public MusicItemView()
        {
            InitializeComponent();
        }

        private void MoreButton_OnClicked(object sender, EventArgs e)
        {


            var imageButton = sender as ImageButton;
            var musicInfo = imageButton.BindingContext as MusicInfo;

            _musicFunctionPage = new MusicFunctionPage(musicInfo, MenuType);
            _musicFunctionPage.OnFinished += MusicFunctionPage_OnFinished;
            this.Popup.ShowPopup(_musicFunctionPage,
                         Constraint.RelativeToParent(c => 0),
                         Constraint.RelativeToParent(c => (Popup.Height - this._musicFunctionPage.HeightRequest))
                         );




        }

        private async void MusicFunctionPage_OnFinished(object sender, MusicFunctionEventArgs e)
        {
            //var libraryPageViewModel = this.BindingContext as LibraryPageViewModel;
            if (e.MusicInfo == null)
            {
                return;
            }
            this.Popup.DismissPopup();
            switch (e.FunctionType)
            {
                case FunctionType.AddToPlaylist:
                    _playlistChoosePage = new PlaylistChoosePage();
                    _playlistChoosePage.OnFinished += (o, c) =>
                    {
                        if (c != null)
                        {
                            MusicInfoServer.Current.CreatePlaylistEntry(e.MusicInfo, c.PlaylistId);


                        }
                        this.Popup.DismissPopup();
                    };

                    this.Popup.ShowPopup(
                        _playlistChoosePage,
                        Constraint.RelativeToParent(c => 0),
                        Constraint.RelativeToParent(c => (Popup.Height - this._playlistChoosePage.HeightRequest))
                        );



                    break;
                case FunctionType.GoAlbumPage:
                    if (OnJumptoOtherPage != null) OnJumptoOtherPage(this, e);
                    
                    break;
                case FunctionType.GoArtistPage:
                    if (OnJumptoOtherPage != null) OnJumptoOtherPage(this, e);
                    
                    break;
                default:
                    break;
            }
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
