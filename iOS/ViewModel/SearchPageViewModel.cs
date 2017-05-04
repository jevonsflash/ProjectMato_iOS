using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class SearchPageViewModel : ViewModelBase
    {
        public SearchPageViewModel()
        {
            this.ItemSelectedCommand = new Common.RelayCommand(c => true, ItemSelectedAction);

        }

        private void ItemSelectedAction(object obj)
        {


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
                musics = value;

                RaisePropertyChanged();
            }
        }

        private MusicInfo _selectedItem;


        public MusicInfo SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                base.RaisePropertyChanged();
            }
        }
        public Common.RelayCommand ItemSelectedCommand { get; set; }

    }
}