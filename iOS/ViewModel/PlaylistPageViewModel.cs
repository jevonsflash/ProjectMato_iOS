using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS
{
    public class PlaylistPageViewModel : BaseViewModel
    {
        public PlaylistPageViewModel()
        {
            this.CreateCommand = new RelayCommand(c => true, CreateAction);
            this.DeleteCommand = new RelayCommand(c => true, DeleteAction);
            this.RenameCommand = new RelayCommand(c => true, RenameAction);
        }

        private void RenameAction(object obj)
        {
            
        }

        private void DeleteAction(object obj)
        {
            
        }

        private void CreateAction(object obj)
        {
            
           MusicInfoServer.Current.CreatePlaylist(new PlaylistTable("测试列表"));
           
            
        }

        private List<PlaylistTable> _playlists;

        public List<PlaylistTable> Playlists
        {
            get
            {
                if (_playlists == null)
                {

                    InitPlaylist();
                }
                return _playlists;

            }
            set
            {

                SetObservableProperty(ref _playlists, value);

            }
        }

        private void InitPlaylist()
        {

            Playlists = MusicInfoServer.Current.GetPlaylist();
            if (Playlists.Count==0)
            {
                MusicInfoServer.Current.CreatePlaylist(new PlaylistTable("我最喜爱"));

            }
            var aa = MusicInfoServer.Current.GetPlaylistEntryFormMyFavourite();
            
        }

        public RelayCommand CreateCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand RenameCommand { get; set; }
    }
}
