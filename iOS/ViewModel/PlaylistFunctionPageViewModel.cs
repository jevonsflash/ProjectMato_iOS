using System;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class PlaylistFunctionPageViewModel : BaseViewModel
    {
        public PlaylistFunctionPageViewModel()
        {
            this.SubmitCommand = new RelayCommand(c => true, new Action<object>(SubmitAction));

        }
        private string _playlistTitle;
        public string PlaylistTitle
        {
            get { return _playlistTitle; }
            set { base.SetObservableProperty(ref _playlistTitle, value); }
        }
        private void SubmitAction(object obj)
        {
            MusicInfoServer.Current.CreatePlaylist(new PlaylistTable(this.PlaylistTitle, false, true));
        }

        public RelayCommand SubmitCommand { get; set; }
    }
}