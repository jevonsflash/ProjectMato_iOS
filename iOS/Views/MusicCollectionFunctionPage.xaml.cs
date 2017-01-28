using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.ViewModel;
using UIKit;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class MusicCollectionFunctionPage
    {
        public MusicCollectionFunctionPage()
        {
            InitializeComponent();
        }
        private MusicCollectionInfo _musicCollectionInfo;
        public event EventHandler<MusicCollectionFunctionEventArgs> OnFinished;
        public MusicCollectionFunctionPage(MusicCollectionInfo musicInfo, MusicCollectionFunctionMenuType type)
        {
            InitializeComponent();
            this._musicCollectionInfo = musicInfo;
            this.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.CancelButton.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.BindingContext = new MusicCollectionFunctionPageViewModel(musicInfo);


            if (type == MusicCollectionFunctionMenuType.Artist)
            {
                this.Play.Text = "播放此艺术家曲目";
            }
            else if (type == MusicCollectionFunctionMenuType.Album)
            {
                this.Play.Text = "播放此专辑";
            }
            else if (type == MusicCollectionFunctionMenuType.Playlist || type == MusicCollectionFunctionMenuType.Queue)
            {
                this.DeleteButton.IsVisible = true;
            }
        }

        private void AddToPlaylistButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicCollectionFunctionEventArgs(this._musicCollectionInfo, MusicCollectionFunctionType.AddToPlaylist));
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicCollectionFunctionEventArgs(this._musicCollectionInfo, MusicCollectionFunctionType.Cancel));
        }


        private void AddToFavourite_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicCollectionFunctionEventArgs(this._musicCollectionInfo, MusicCollectionFunctionType.AddToFavourite));
        }

        private void AddToQueueButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicCollectionFunctionEventArgs(this._musicCollectionInfo, MusicCollectionFunctionType.AddToQueue));
        }

        private void Play_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicCollectionFunctionEventArgs(this._musicCollectionInfo, MusicCollectionFunctionType.Play));
        }

        private void DeleteButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new MusicCollectionFunctionEventArgs(this._musicCollectionInfo, MusicCollectionFunctionType.Delete));
        }
    }

    public class MusicCollectionFunctionEventArgs : EventArgs
    {
        public MusicCollectionFunctionEventArgs(MusicCollectionInfo musicInfo, MusicCollectionFunctionType functionType)
        {
            this.MusicCollectionInfo = musicInfo;
            this.FunctionType = functionType;
        }
        public MusicCollectionInfo MusicCollectionInfo { get; set; }
        public MusicCollectionFunctionType FunctionType { get; set; }
    }

    public enum MusicCollectionFunctionType
    {
        AddToPlaylist, AddToQueue, Cancel, AddToFavourite, Play, Delete
    }


    public enum MusicCollectionFunctionMenuType
    {
        Album, Artist, Full, Playlist, Queue
    }
}
