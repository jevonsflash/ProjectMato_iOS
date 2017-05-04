using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using CoreMedia;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class PlaylistEntryPageViewModel : ViewModelBase
    {
        public PlaylistEntryPageViewModel(PlaylistInfo playlist)
        {
            Playlist = playlist;
            Musics.CollectionChanged += Musics_CollectionChanged;

        }
        public void DeleteAction(object obj)
        {
            var musicInfo = obj as MusicInfo;
            this.Musics.Remove(musicInfo);
        }

        private void Musics_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                var oldIndex = e.OldStartingIndex;
                var newIndex = e.NewStartingIndex;
                MusicInfoServer.Current.ReorderPlaylist(Musics[oldIndex], Musics[newIndex], Playlist.Id);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MusicInfoServer.Current.DeletePlaylistEntry(e.OldItems[0] as MusicInfo, Playlist.Id);
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
                playlist = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollectionEx<MusicInfo> musics;

        public ObservableCollectionEx<MusicInfo> Musics
        {
            get
            {
                if (musics == null)
                {
                    musics = new ObservableCollectionEx<MusicInfo>(MusicInfoServer.Current.GetPlaylistEntry(this.Playlist.Id));
                }
                return musics;
            }
            set
            {
                musics = value;
                RaisePropertyChanged();
            }
        }
    }
}
