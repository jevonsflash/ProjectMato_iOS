using System;
using System.Collections.Generic;
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
        }


        private List<MusicInfo> musics;


        public List<MusicInfo> Musics
        {
            get
            {
                if (musics == null || musics.Count == 0)
                {
                    musics = MusicSystem.MusicInfos;
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
