﻿using ProjectMato.iOS.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVFoundation;
using Foundation;
using ProjectMato.iOS.Model;

namespace ProjectMato.iOS
{
    public class MusicSystem
    {
        public static event EventHandler<bool> OnPlayFinished;
        private static NSError nserror = new NSError();

        private static void OnFinishedPlaying(Object sender, AVStatusEventArgs e)
        {
            OnPlayFinished?.Invoke(null, e.Status);
        }


        private static int[] shuffleMap;

        public static int[] ShuffleMap
        {
            get
            {
                if (shuffleMap == null || shuffleMap.Length == 0)
                {
                    shuffleMap = CommonServer.Current.GetRandomArry(0, LastIndex);
                }
                return shuffleMap;
            }
        }
        private static AVAudioPlayer currentPlayer;

        public static AVAudioPlayer CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }

        }

        private static List<MusicInfo> musicInfos;

        public static List<MusicInfo> MusicInfos
        {
            get
            {
                //if (musicInfos == null || musicInfos.Count == 0)
                //{
                RebuildMusicInfos();
                // }
                return musicInfos;
            }
        }

        public static void RebuildMusicInfos()
        {
            musicInfos = MusicInfoServer.Current.GetQueueEntry();
        }

        public static int LastIndex { get { return MusicInfos.FindLastIndex(c => true); } }

        public static MusicInfo GetNextMusic(MusicInfo current, bool isShuffle)
        {
            MusicInfo currentMusicInfo = null;
            var index = GetMusicIndex(current);

            if (isShuffle)
            {
                if (index + 1 > LastIndex)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else
            {
                index = GetShuffleMusicIndex(index, 1);
            }

            if (MusicInfos.Count != 0)
            {
                currentMusicInfo = MusicInfos[index];
            }
            return currentMusicInfo;
        }

        public static MusicInfo GetPreMusic(MusicInfo current, bool isShuffle)
        {
            MusicInfo currentMusicInfo = null;
            var index = GetMusicIndex(current);
            if (isShuffle)
            {
                if (index - 1 < 0)
                {
                    index = LastIndex;
                }
                else
                {
                    index--;
                }
            }
            else
            {
                index = GetShuffleMusicIndex(index, -1);
            }

            if (MusicInfos.Count != 0)
            {
                currentMusicInfo = MusicInfos[index];
            }

            return currentMusicInfo;
        }

        public static int GetMusicIndex(MusicInfo musicInfo)
        {
            var result = MusicInfos.IndexOf(MusicInfos.FirstOrDefault(c => c.Id == musicInfo.Id));
            return result;
        }

        public static MusicInfo GetMusicByIndex(int index)
        {
            var result = MusicInfos[index];
            return result;
        }

        public static void InitPlayer(MusicInfo CurrentMusic)
        {
            currentPlayer = new AVAudioPlayer(new NSUrl(CurrentMusic.Url), "", out nserror);
            currentPlayer.NumberOfLoops = 2;
            CurrentPlayer.FinishedPlaying -= new EventHandler<AVStatusEventArgs>(OnFinishedPlaying);
            CurrentPlayer.FinishedPlaying += new EventHandler<AVStatusEventArgs>(OnFinishedPlaying);


        }

        public static void Play(MusicInfo currentMusic)
        {
            if (currentMusic != null)
            {
                if (CurrentPlayer != null)
                {
                    Stop();
                }
                InitPlayer(currentMusic);
                CurrentPlayer.Play();
            }
        }

        public static void Stop()
        {
            if (CurrentPlayer.Playing)
            {
                CurrentPlayer.Stop();
            }
        }

        public static void PauseOrResume()
        {

            var status = CurrentPlayer.Playing;
            PauseOrResume(status);
        }

        public static void PauseOrResume(bool status)
        {
            if (status)
            {
                CurrentPlayer.Pause();
            }
            else
            {
                CurrentPlayer.Play();
            }
        }

        private static int GetShuffleMusicIndex(int originItem, int increment)
        {
            var originItemIndex = 0;

            foreach (var item in ShuffleMap)
            {
                if (originItem == item)
                {
                    break;
                }
                originItemIndex++;
            }
            originItemIndex += increment;
            var resultContent = ShuffleMap[originItemIndex];
            return resultContent;
        }

        public static void UpdateShuffleMap()
        {
            shuffleMap = CommonServer.Current.GetRandomArry(0, LastIndex);
        }
    }
}
