using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class QueuePageViewModel : BaseViewModel
    {

        public QueuePageViewModel()
        {
            this.DeleteCommand = new RelayCommand(c => true, DeleteAction);
        }

        public void DeleteAction(object obj)
        {
            MusicInfoServer.Current.DeleteMusicInfoFormQueueEntry(obj as MusicInfo);
            MusicSystem.RebuildMusicInfos();
            Musics = new ObservableCollection<MusicInfo>(MusicSystem.MusicInfos);
        }


        private ObservableCollection<MusicInfo> musics;


        public ObservableCollection<MusicInfo> Musics
        {
            get
            {
                if (musics == null)
                {
                    musics = new ObservableCollection<MusicInfo>(MusicSystem.MusicInfos);
                }
                return musics;
            }
            set
            {


                SetObservableProperty(ref musics, value);
            }
        }
        public RelayCommand DeleteCommand { get; set; }


    }
}
