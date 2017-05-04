using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Server;
using Xamarin.Forms;
using XLabs;

namespace ProjectMato.iOS
{
    public class LibraryPageViewModel : ViewModelBase
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

        private List<MusicInfo> _musics;

        public List<MusicInfo> Musics
        {
            get
            {
                if (_musics == null)
                {
                    var result = MusicInfoServer.Current.GetMusicInfos();
                    _musics = result;
                }
                return _musics;

            }
            set
            {

                _musics = value;
                RaisePropertyChanged();

            }
        }

        private List<AlbumInfo> _albums;

        public List<AlbumInfo> Albums
        {
            get
            {
                if (_albums == null)
                {

                    InitAlbums();
                }
                return _albums;

            }
            set
            {
                _albums = value;
                RaisePropertyChanged();

            }
        }

        private List<ArtistInfo> _artists;
        public List<ArtistInfo> Artists
        {
            get
            {
                if (_artists == null)
                {

                    InitArtists();
                }
                return _artists;

            }
            set
            {
                _artists = value;
                RaisePropertyChanged();
            }
        }

        private AlphaGroupedObservableCollection<MusicInfo> _aGMusics;
        public AlphaGroupedObservableCollection<MusicInfo> AGMusics
        {
            get
            {
                if (_aGMusics == null)
                {

                    InitMusics();
                }
                return _aGMusics;

            }
            set
            {
                _aGMusics = value;
                RaisePropertyChanged();

            }
        }

        private AlphaGroupedObservableCollection<ArtistInfo> _aGArtists;
        public AlphaGroupedObservableCollection<ArtistInfo> AGArtists
        {
            get
            {
                if (_aGArtists == null)
                {

                    InitArtists();
                }
                return _aGArtists;

            }
            set
            {
                _aGArtists = value;
                RaisePropertyChanged();

            }
        }

        private AlphaGroupedObservableCollection<AlbumInfo> _aGAlbums;
        public AlphaGroupedObservableCollection<AlbumInfo> AGAlbums
        {
            get
            {
                if (_aGAlbums == null)
                {

                    InitAlbums();
                }
                return _aGAlbums;

            }
            set
            {
                _aGAlbums = value;
                RaisePropertyChanged();

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
                _isShowGrid = value;
                RaisePropertyChanged();

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
