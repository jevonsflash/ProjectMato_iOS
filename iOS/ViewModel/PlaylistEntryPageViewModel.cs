﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using CoreMedia;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class PlaylistEntryPageViewModel : BaseViewModel
    {
        public PlaylistEntryPageViewModel(PlaylistInfo playlist)
        {
            Playlist = playlist;
            Musics.CollectionChanged += Musics_CollectionChanged;

        }


        private void Musics_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                var oldIndex = e.OldStartingIndex;
                var newIndex = e.NewStartingIndex;
                MusicInfoServer.Current.ReorderPlaylist(Musics[oldIndex], Musics[newIndex], Playlist.PlaylistId);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MusicInfoServer.Current.DeletePlaylistEntry(e.OldItems[0] as MusicInfo, Playlist.PlaylistId);
            }


        }

        private PlaylistInfo playlist;

        public PlaylistInfo Playlist
        {
            get
            {
                return playlist;
            }
            set
            {
                SetObservableProperty(ref playlist, value);
            }
        }
        private ObservableCollectionEx<MusicInfo> musics;

        public ObservableCollectionEx<MusicInfo> Musics
        {
            get
            {
                if (musics == null)
                {
                    musics = new ObservableCollectionEx<MusicInfo>(MusicInfoServer.Current.GetPlaylistEntry(this.Playlist.PlaylistId));
                }
                return musics;
            }
            set
            {
                SetObservableProperty(ref musics, value);
            }
        }
    }
}
