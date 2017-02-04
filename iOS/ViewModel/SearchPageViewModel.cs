using System;
using System.Collections.Generic;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class SearchPageViewModel : BaseViewModel
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

                SetObservableProperty(ref musics, value);

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
                base.SetObservableProperty(ref _selectedItem, value);
            }
        }
        public Common.RelayCommand ItemSelectedCommand { get; set; }

    }
}