using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class MusicFunctionPageViewModel : ViewModelBase
    {
        public MusicFunctionPageViewModel(MusicInfo musicInfo, IList<MenuCellInfo> mainMenuCellInfos)
        {
            this.MusicInfo = musicInfo;
            this.MainMenuCellInfos = mainMenuCellInfos;
            this.PropertyChanged += MusicFunctionPageViewModel_PropertyChanged;
        }

        private void MusicFunctionPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentMenuCellInfo))
            {
                //
            }
        }

        private void AddToQueueAction(object obj)
        {
            MusicInfoServer.Current.CreateQueueEntry(this.MusicInfo);
        }

        private void NextPlayAction(object obj)
        {

        }

        private MusicInfo _musicInfo;
        public MusicInfo MusicInfo
        {
            get { return _musicInfo; }
            set
            {
                _musicInfo = value;
                base.RaisePropertyChanged();
            }
        }


        private IList<MenuCellInfo> _mainMenuCellInfos;

        public IList<MenuCellInfo> MainMenuCellInfos
        {
            get
            {
                if (_mainMenuCellInfos == null)
                {
                    _mainMenuCellInfos = new List<MenuCellInfo>();
                }
                return _mainMenuCellInfos;
            }
            set
            {
                _mainMenuCellInfos = value;
                base.RaisePropertyChanged();


            }
        }

        private MenuCellInfo _currentMenuCellInfo;

        public MenuCellInfo CurrentMenuCellInfo
        {
            get
            {
                if (_currentMenuCellInfo == null)
                {
                    _currentMenuCellInfo = MainMenuCellInfos[1];
                }
                return _currentMenuCellInfo;
            }
            set
            {
                _currentMenuCellInfo = value;
                base.RaisePropertyChanged();
            }
        }

    }
}
