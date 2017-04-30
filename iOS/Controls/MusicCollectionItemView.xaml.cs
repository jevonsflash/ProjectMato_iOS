using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS.Controls
{

    public partial class MusicCollectionItemView
    {
        private PlaylistFunctionPage _playlistFunctionPage;
        private PlaylistChoosePage _playlistChoosePage;

        private MusicCollectionFunctionPage _musicCollectionFunctionPage;
        public MusicCollectionFunctionMenuType MenuType { get; set; }
        public event EventHandler<MusicCollectionFunctionEventArgs> OnFinishedChoice;

        public MusicCollectionItemView(MusicCollectionFunctionMenuType menuType) : this()
        {

            this.MenuType = menuType;

        }

        public MusicCollectionItemView()
        {
            InitializeComponent();
        }

        private void MoreButton_OnClicked(object sender, EventArgs e)
        {


            var imageButton = sender as ImageButton;
            var musicInfo = imageButton.BindingContext as MusicCollectionInfo;

            _musicCollectionFunctionPage = new MusicCollectionFunctionPage(musicInfo, MenuType);
            _musicCollectionFunctionPage.OnFinished += MusicCollectionFunctionPage_OnFinished;

            //this.Popup.ShowPopup(_musicCollectionFunctionPage);
        }

        private async void MusicCollectionFunctionPage_OnFinished(object sender, MusicCollectionFunctionEventArgs e)
        {
            if (e.MusicCollectionInfo == null)
            {
                return;
            }
            //this.Popup.HidePopup();

            if (e.FunctionType == MusicCollectionFunctionType.AddToPlaylist)
            {

                _playlistChoosePage = new PlaylistChoosePage();
                _playlistChoosePage.OnFinished += (o, c) =>
                {
                    if (c != null)
                    {
                        MusicInfoServer.Current.CreatePlaylistEntrys(e.MusicCollectionInfo, c.PlaylistId);
                    }
                    //this.Popup.HidePopup();
                };

                ////this.Popup.ShowPopup(
                //    _playlistChoosePage

                //    );
            }

            else
            {
                if (OnFinishedChoice != null)
                    OnFinishedChoice(sender, e);
            }
        }
    }

}
