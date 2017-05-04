using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class QueuePageViewModel : ViewModelBase
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
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MusicInfoServer.Current.DeleteMusicInfoFormQueueEntry(e.OldItems[0] as MusicInfo);
            }

        }

        public void DeleteAction(object obj)
        {
            Musics.Remove(obj as MusicInfo);
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
                musics = value;


                RaisePropertyChanged();
            }
        }

        public RelayCommand DeleteCommand { get; set; }
    }
}
