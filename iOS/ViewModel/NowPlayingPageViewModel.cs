using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Common;

namespace ProjectMato.iOS.ViewModel
{
    public class NowPlayingPageViewModel : ViewModelBase
    {
        public NowPlayingPageViewModel()
        {
            this.SwitchPannelCommand = new RelayCommand(c => true, SwitchPannelAction);
            IsLrcPanel = false;
        }

        private void SwitchPannelAction(object obj)
        {
            IsLrcPanel = true;
        }

        private MusicRelatedViewModel _currentMusicRelatedViewModel;

        public MusicRelatedViewModel CurrentMusicRelatedViewModel
        {
            get
            {
                if (_currentMusicRelatedViewModel == null)
                {
                    _currentMusicRelatedViewModel = MusicRelatedViewModel.Current;
                }
                return _currentMusicRelatedViewModel;
            }
            set
            {
                _currentMusicRelatedViewModel = value;
                RaisePropertyChanged();
            }
        }

        private bool _isLrcPanel;

        public bool IsLrcPanel
        {
            get { return _isLrcPanel; }
            set
            {
                _isLrcPanel = value;
                RaisePropertyChanged();

            }
        }

        public RelayCommand SwitchPannelCommand { get; set; }


    }
}
