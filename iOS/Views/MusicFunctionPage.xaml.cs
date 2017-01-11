using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.ViewModel;
using UIKit;
using Xamarin.Forms;
using XLabs.Platform.Device;


namespace ProjectMato.iOS
{
    public partial class MusicFunctionPage
    {
        private MusicInfo _musicInfo;
        public event EventHandler<MusicFunctionEventArgs> OnFinished;
        public MusicFunctionPage(MusicInfo musicInfo, FunctionMenuType type)
        {
            InitializeComponent();
            this._musicInfo = musicInfo;
            this.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.CancelButton.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.BindingContext = new MusicFunctionPageViewModel(musicInfo);


            if (type == FunctionMenuType.Artist)
            {
                this.GoArtistPageButton.IsVisible = false;
            }
            else if (type == FunctionMenuType.Album)
            {
                this.GoAlbumPageButton.IsVisible = false;
            }
            else if (type == FunctionMenuType.NowPlaying)
            {
                this.NextPlayButton.IsEnabled = false;
                this.AddToQueueButton.IsEnabled = false;
            }

            if (string.IsNullOrEmpty(musicInfo.Artist))
            {
                this.GoArtistPageButton.IsVisible = false;
            }
            if (string.IsNullOrEmpty(musicInfo.AlbumTitle))
            {
                this.GoAlbumPageButton.IsVisible = false;
            }
        }

        private void AddToPlaylistButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, FunctionType.AddToPlaylist));
        }

        private void GoArtistPageButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, FunctionType.GoArtistPage));

        }

        private void GoAlbumPageButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, FunctionType.GoAlbumPage));

        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, FunctionType.Cancel));
        }
    }
    public class MusicFunctionEventArgs : EventArgs
    {
        public MusicFunctionEventArgs(MusicInfo musicInfo, FunctionType functionType)
        {
            this.MusicInfo = musicInfo;
            this.FunctionType = functionType;
        }
        public MusicInfo MusicInfo { get; set; }
        public FunctionType FunctionType { get; set; }
    }
    public enum FunctionType
    {
        AddToPlaylist, NextPlay, AddToQueue, GoArtistPage, GoAlbumPage, Cancel
    }


    public enum FunctionMenuType
    {
        NowPlaying, Album, Artist, Full
    }


}
