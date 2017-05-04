using System;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class PlaylistFunctionPageViewModel : ViewModelBase
    {
        public PlaylistFunctionPageViewModel()
        {
            this.SubmitCommand = new RelayCommand(c => true, new Action<object>(SubmitAction));
            this.PlaylistInfo = new PlaylistInfo() { IsHidden = false, IsRemovable = true, Title = "新建歌单" };
        }

        public PlaylistFunctionPageViewModel(PlaylistInfo playlistInfo) : this()
        {
            this.PlaylistInfo = playlistInfo;
        }
        private PlaylistInfo _playlistInfo;
        public PlaylistInfo PlaylistInfo
        {
            get { return _playlistInfo; }
            set
            {
                _playlistInfo = value;
                base.RaisePropertyChanged();
            }
        }
        private void SubmitAction(object obj)
        {
            //MusicInfoServer.Current.CreatePlaylist(PlaylistInfo);
        }

        public RelayCommand SubmitCommand { get; set; }
    }
}