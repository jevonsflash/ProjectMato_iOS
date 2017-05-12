using ProjectMato.iOS.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVFoundation;
using Foundation;
using ProjectMato.iOS.Helper;
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
                    shuffleMap = CommonHelper.GetRandomArry(0, LastIndex);
                }
                return shuffleMap;
            }
        }
        private static AVAudioPlayer currentPlayer;

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


        public static double Duration { get { return currentPlayer.Duration; } }


        public static double CurrentTime { get { return currentPlayer.CurrentTime; } }


        public static bool IsPlaying { get { return currentPlayer.Playing; } }

        public static void SeekTo(double position)

        {
            currentPlayer.CurrentTime = position;
        }

        public static MusicInfo GetNextMusic(MusicInfo current, bool isShuffle)
        {
            MusicInfo currentMusicInfo = null;
            var index = GetMusicIndex(current);

            if (!isShuffle)
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
            if (!isShuffle)
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
            AVAudioSession.SharedInstance().SetCategory(AVAudioSessionCategory.Playback);
            AVAudioSession.SharedInstance().SetActive(true);
            currentPlayer = new AVAudioPlayer(new NSUrl(CurrentMusic.Url), "", out nserror);
            //注册完成播放事件
            currentPlayer.FinishedPlaying -= new EventHandler<AVStatusEventArgs>(OnFinishedPlaying);
            currentPlayer.FinishedPlaying += new EventHandler<AVStatusEventArgs>(OnFinishedPlaying);


        }

        public static void Play(MusicInfo currentMusic)
        {
            if (currentMusic != null)
            {
                if (currentPlayer != null)
                {
                    Stop();
                }
                InitPlayer(currentMusic);
                currentPlayer.Play();
            }
        }

        public static void Stop()
        {
            if (currentPlayer.Playing)
            {
                currentPlayer.Stop();
            }
        }

        public static void PauseOrResume()
        {

            var status = currentPlayer.Playing;
            PauseOrResume(status);
        }

        public static void PauseOrResume(bool status)
        {

            if (status)
            {
                currentPlayer.Pause();
            }
            else
            {
                currentPlayer.Play();
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
            var newItemIndex = originItemIndex + increment;
            if (newItemIndex < 0)
            {
                newItemIndex = LastIndex;
            }
            if (newItemIndex > LastIndex)
            {
                newItemIndex = 0;
            }
            try
            {
                var resultContent = ShuffleMap[newItemIndex];
                return resultContent;

            }
            catch (Exception e)
            {
                return ShuffleMap[0];
            }
        }

        public static void UpdateShuffleMap()
        {
            shuffleMap = CommonHelper.GetRandomArry(0, LastIndex);
        }

        public static void SetRepeatOneStatus(bool isRepeatOne)
        {
            if (currentPlayer != null)
            {
                currentPlayer.NumberOfLoops = isRepeatOne ? nint.MaxValue : 0;
            }
        }
    }
}
