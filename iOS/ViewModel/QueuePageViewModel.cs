using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            this.Musics.CollectionChanged += Musics_CollectionChanged;
        }

        private void Musics_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                
                var oldIndex = e.OldStartingIndex;
                var newIndex = e.NewStartingIndex;
                MusicInfoServer.Current.ReorderQueue(Musics[oldIndex], Musics[newIndex]);

            }

        }

        public void DeleteAction(object obj)
        {
            MusicInfoServer.Current.DeleteMusicInfoFormQueueEntry(obj as MusicInfo);
            MusicSystem.RebuildMusicInfos();
            Musics = new ObservableCollectionEx<MusicInfo>(MusicSystem.MusicInfos);
        }


        private ObservableCollectionEx<MusicInfo> musics;


        public ObservableCollectionEx<MusicInfo> Musics
        {
            get
            {
                if (musics == null)
                {
                    musics = new ObservableCollectionEx<MusicInfo>(MusicSystem.MusicInfos);
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
