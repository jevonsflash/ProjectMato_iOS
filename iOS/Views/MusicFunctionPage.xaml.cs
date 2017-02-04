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
        public MusicFunctionPage(MusicInfo musicInfo, MusicFunctionMenuType type)
        {
            InitializeComponent();
            this._musicInfo = musicInfo;
            this.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.CancelButton.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.BindingContext = new MusicFunctionPageViewModel(musicInfo);


            if (type == MusicFunctionMenuType.Artist)
            {
                this.GoArtistPageButton.IsVisible = false;
            }
            else if (type == MusicFunctionMenuType.Album)
            {
                this.GoAlbumPageButton.IsVisible = false;
            }
            else if (type == MusicFunctionMenuType.NowPlaying)
            {
                this.NextPlayButton.IsVisible = false;
                this.AddToQueueButton.IsVisible = false;
            }
            else if (type == MusicFunctionMenuType.Playlist)
            {
                this.DeleteButton.IsVisible = true;

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
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, MusicFunctionType.AddToPlaylist));
        }

        private void GoArtistPageButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, MusicFunctionType.GoArtistPage));

        }

        private void GoAlbumPageButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, MusicFunctionType.GoAlbumPage));

        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, MusicFunctionType.Cancel));
        }

        private void AddToQueueButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, MusicFunctionType.AddToQueue));
        }

        private void NextPlayButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, MusicFunctionType.NextPlay));
        }

        private void DeleteButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicFunctionEventArgs(this._musicInfo, MusicFunctionType.Delete));
        }
    }
    public class MusicFunctionEventArgs : EventArgs
    {
        public MusicFunctionEventArgs(MusicInfo musicInfo, MusicFunctionType functionType)
        {
            this.MusicInfo = musicInfo;
            this.FunctionType = functionType;
        }
        public MusicInfo MusicInfo { get; set; }
        public MusicFunctionType FunctionType { get; set; }
    }
    public enum MusicFunctionType
    {
        AddToPlaylist, NextPlay, AddToQueue, GoArtistPage, GoAlbumPage, Cancel, Delete
    }


    public enum MusicFunctionMenuType
    {
        NowPlaying, Album, Artist, Full, Playlist
    }


}
