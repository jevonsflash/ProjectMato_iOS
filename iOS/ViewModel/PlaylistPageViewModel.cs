﻿using System;
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
            this.DeleteCommand = new RelayCommand(c => true, DeleteAction);

            Playlists.CollectionChanged += Playlists_CollectionChanged;
        }

        private void Playlists_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MusicInfoServer.Current.DeletePlaylist(e.OldItems[0] as PlaylistInfo);
            }
        }


        public void DeleteAction(object obj)
        {
            var playlistInfo = obj as PlaylistInfo;
            if (playlistInfo.Title != "我最喜爱")
                Playlists.Remove(obj as PlaylistInfo);
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

        public RelayCommand DeleteCommand { get; set; }

    }
}
