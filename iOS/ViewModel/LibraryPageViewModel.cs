using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Server;
using Xamarin.Forms;
using XLabs;

namespace ProjectMato.iOS
{
    public class LibraryPageViewModel : BaseViewModel
    {
        public LibraryPageViewModel()
        {
            this.PlayAllCommand = new Common.RelayCommand(CanPlayAll, PlayAllAction);
        }

        private void PlayAllAction(object obj)
        {
            MusicInfoServer.Current.ClearQueue();
            MusicInfoServer.Current.CreateQueueEntrys(this.Musics);
            MusicRelatedViewModel.Current.CurrentMusic = MusicInfoServer.Current.GetQueueEntry()[0];
        }

        private bool CanPlayAll(object obj)
        {
            //var result = this.Musics.Count > 0;
            return true;

        }

        private List<MusicInfo> musics;

        public List<MusicInfo> Musics
        {
            get
            {
                if (musics == null)
                {
                    var result = MusicInfoServer.Current.GetMusicInfos();
                    musics = result;
                }
                return musics;

            }
            set
            {

                SetObservableProperty(ref musics, value);

            }
        }

        private List<AlbumInfo> albums;

        public List<AlbumInfo> Albums
        {
            get
            {
                if (albums == null)
                {

                    InitAlbums();
                }
                return albums;

            }
            set
            {

                SetObservableProperty(ref albums, value);

            }
        }

        private List<ArtistInfo> artists;
        public List<ArtistInfo> Artists
        {
            get
            {
                if (artists == null)
                {

                    InitArtists();
                }
                return artists;

            }
            set
            {

                SetObservableProperty(ref artists, value);

            }
        }

        private AlphaGroupedObservableCollection<MusicInfo> aGMusics;
        public AlphaGroupedObservableCollection<MusicInfo> AGMusics
        {
            get
            {
                if (aGMusics == null)
                {

                    InitMusics();
                }
                return aGMusics;

            }
            set
            {

                SetObservableProperty(ref aGMusics, value);

            }
        }

        private AlphaGroupedObservableCollection<ArtistInfo> aGArtists;
        public AlphaGroupedObservableCollection<ArtistInfo> AGArtists
        {
            get
            {
                if (aGArtists == null)
                {

                    InitArtists();
                }
                return aGArtists;

            }
            set
            {

                SetObservableProperty(ref aGArtists, value);

            }
        }

        private AlphaGroupedObservableCollection<AlbumInfo> aGAlbums;
        public AlphaGroupedObservableCollection<AlbumInfo> AGAlbums
        {
            get
            {
                if (aGAlbums == null)
                {

                    InitAlbums();
                }
                return aGAlbums;

            }
            set
            {

                SetObservableProperty(ref aGAlbums, value);

            }
        }
        private bool _isShowGrid;

        public bool IsShowGrid
        {
            get
            {
              
                return _isShowGrid;

            }
            set
            {

                SetObservableProperty(ref _isShowGrid, value);

            }
        }
        private void InitArtists()
        {
            Artists = MusicInfoServer.Current.GetArtistInfos();
            AGArtists = MusicInfoServer.Current.GetAlphaGroupedArtistInfo();
        }

        private void InitAlbums()
        {
            Albums = MusicInfoServer.Current.GetAlbumInfos();
            AGAlbums = MusicInfoServer.Current.GetAlphaGroupedAlbumInfo();
        }

        private void InitMusics()
        {
            Musics = MusicInfoServer.Current.GetMusicInfos();
            AGMusics = MusicInfoServer.Current.GetAlphaGroupedMusicInfo();
        }

        public Common.RelayCommand PlayAllCommand { get; set; }

    }
}
