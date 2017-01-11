using System;
using System.Collections.Generic;
using System.Text;
using CoreMedia;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
   public class PlaylistEntryPageViewModel:BaseViewModel
    {
       public PlaylistEntryPageViewModel(PlaylistTable playlistTable)
       {
           Playlist = playlistTable;
          
       }

        private PlaylistTable playlist;

        public PlaylistTable Playlist
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
        private List<MusicInfo> musics;

        public List<MusicInfo> Musics
        {
            get
            {
                if (musics==null)
                {
                     musics = MusicInfoServer.Current.GetPlaylistEntry(this.Playlist.PlaylistId);
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
