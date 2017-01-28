using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

            Playlists.CollectionChanged += Playlists_CollectionChanged;
        }

        private void Playlists_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        private void RenameAction(object obj)
        {

        }

        private void DeleteAction(object obj)
        {

        }

        private void CreateAction(object obj)
        {



        }

        private ObservableCollectionEx<PlaylistInfo> _playlists;

        public ObservableCollectionEx<PlaylistInfo> Playlists
        {
            get
            {
                if (_playlists == null)
                {

                    _playlists = new ObservableCollectionEx<PlaylistInfo>(InitPlaylist());
                }
                return _playlists;

            }
            set
            {

                SetObservableProperty(ref _playlists, value);

            }
        }

        private List<PlaylistInfo> InitPlaylist()
        {
            var restul = MusicInfoServer.Current.GetPlaylistInfo();
            if (restul.Count == 0 || !restul.Any(c => c.Title == "我最喜爱"))
            {
                MusicInfoServer.Current.CreatePlaylist(new PlaylistTable("我最喜爱", false, false));
                restul = MusicInfoServer.Current.GetPlaylistInfo();
            }
            return restul;
        }

        public RelayCommand CreateCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand RenameCommand { get; set; }
    }
}
