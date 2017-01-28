using System;
using System.Collections.Generic;
using System.Text;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class MusicFunctionPageViewModel : BaseViewModel
    {
        public MusicFunctionPageViewModel(MusicInfo musicInfo)
        {
            this.MusicInfo = musicInfo;
            this.NextPlayCommand = new RelayCommand(c => true, new Action<object>(NextPlayAction));
            this.AddToQueueCommand = new RelayCommand(c => true, new Action<object>(AddToQueueAction));
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
            set { base.SetObservableProperty(ref _musicInfo, value); }
        }

        public RelayCommand NextPlayCommand { get; set; }
        public RelayCommand AddToQueueCommand { get; set; }

    }
}
