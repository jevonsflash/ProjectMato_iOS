using System;
using System.Linq;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class MusicCollectionFunctionPageViewModel : BaseViewModel
    {
        public MusicCollectionFunctionPageViewModel(MusicCollectionInfo musicCollectionInfo)
        {
            this.MusicCollectionInfo = musicCollectionInfo;
            this.NextPlayCommand = new RelayCommand(c => true, new Action<object>(NextPlayAction));
            this.AddToQueueCommand = new RelayCommand(c => true, new Action<object>(AddToQueueAction));
            this.PlayCommand = new RelayCommand(c => true, new Action<object>(PlayAction));
        }

        private void AddToQueueAction(object obj)
        {
            MusicInfoServer.Current.CreateQueueEntrys(this.MusicCollectionInfo.Musics.ToList());
        }

        private void NextPlayAction(object obj)
        {

        }

        private void PlayAction(object obj)
        {
            MusicInfoServer.Current.ClearQueue();
            MusicInfoServer.Current.CreateQueueEntrys(this.MusicCollectionInfo.Musics.ToList());
            MusicRelatedViewModel.Current.CurrentMusic = MusicInfoServer.Current.GetQueueEntry()[0];
        }
        private MusicCollectionInfo _musicCollectionInfo;
        public MusicCollectionInfo MusicCollectionInfo
        {
            get { return _musicCollectionInfo; }
            set { base.SetObservableProperty(ref _musicCollectionInfo, value); }
        }

        public RelayCommand NextPlayCommand { get; set; }
        public RelayCommand AddToQueueCommand { get; set; }
        public RelayCommand PlayCommand { get; set; }

    }
}