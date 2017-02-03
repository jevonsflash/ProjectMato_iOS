using AVFoundation;
using Foundation;
using ProjectMato.iOS.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS
{
    public class MusicRelatedViewModel : BaseViewModel
    {
        private static MusicRelatedViewModel current;

        public static MusicRelatedViewModel Current
        {
            get
            {
                if (current == null)
                {
                    current = new MusicRelatedViewModel();
                }
                return current;
            }

        }

        private Timer timer;

        public event EventHandler OnMusicChanged;
        public static class Properties
        {
            public const string CurrentMusic = "CurrentMusic";
            public const string IsPlaying = "IsPlaying";
            public const string CurrentTime = "CurrentTime";
            public const string Duration = "Duration";
            public const string Musics = "Musics";
        }

        private MusicRelatedViewModel()
        {
            timer = new Timer(1000);
            timer.Elapsed += DoUpdate;
            timer.Start();

            this.PlayCommand = new RelayCommand(c => true, PlayAction);
            this.PreCommand = new RelayCommand(c => true, PreAction);
            this.NextCommand = new RelayCommand(c => true, NextAction);
            this.RepeatOneCommand = new RelayCommand(c => true, RepeatOneAction);
            this.ShuffleCommand = new RelayCommand(c => true, ShuffleAction);
            this.PropertyChanged += DetailPageViewModel_PropertyChanged;
            MusicSystem.OnPlayFinished += MusicSystem_OnMusicChanged;
            MusicSystem.SetRepeatOneStatus(IsRepeatOne);
            MusicSystem.UpdateShuffleMap();
            InitCurrentMusic();

        }
        #region methods
        private void MusicSystem_OnMusicChanged(Object sender, bool e)
        {
            NextAction(null);
        }

        private void DoUpdate(object o, EventArgs e)
        {
            this.CurrentTime = MusicSystem.CurrentPlayer.CurrentTime;
        }

        private void DetailPageViewModel_PropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Properties.CurrentMusic)
            {
                AVAudioSession.SharedInstance().SetCategory(AVAudioSessionCategory.Playback);
                AVAudioSession.SharedInstance().SetActive(true);
                MusicSystem.Play(CurrentMusic);
                DoUpdate(this, EventArgs.Empty);
                InitPreviewAndNextMusic();
                if (OnMusicChanged != null)
                    OnMusicChanged(this, EventArgs.Empty);
                this.Duration = MusicSystem.CurrentPlayer.Duration;
                SettingServer.Current.SetSetting(SettingServer.Properties.BreakPointMusicIndex, Musics.IndexOf(CurrentMusic).ToString());
            }
            else if (e.PropertyName == Properties.IsPlaying)
            {
                MusicSystem.PauseOrResume(!IsPlaying);
            }
        }

        private void InitPreviewAndNextMusic()
        {
            this.PreviewMusic = MusicSystem.GetPreMusic(this.CurrentMusic, false);
            this.NextMusic = MusicSystem.GetNextMusic(this.CurrentMusic, false);
        }

        public void NextAction(object obj)
        {
            var next = MusicSystem.GetNextMusic(this.CurrentMusic, IsShuffle);
            this.CurrentMusic = next;
        }

        public void PreAction(object obj)
        {
            var pre = MusicSystem.GetPreMusic(this.CurrentMusic, IsShuffle);
            this.CurrentMusic = pre;
        }

        private void PlayAction(object obj)
        {
            var actionType = obj as string;
            if (actionType == "play")
            {
                IsPlaying = true;
            }
            else if (actionType == "pause")
            {
                IsPlaying = false;
            }


        }

        private void ShuffleAction(object obj)
        {
            var actionType = obj as string;
            if (actionType == "shuffle")
            {
                MusicSystem.UpdateShuffleMap();
                IsShuffle = true;
            }
            else if (actionType == "unshuffle")
            {
                IsShuffle = false;
            }
        }

        private void RepeatOneAction(object obj)
        {
            var actionType = obj as string;
            if (actionType == "repeatone")
            {
                IsRepeatOne = true;

            }
            else if (actionType == "unrepeatone")
            {
                IsRepeatOne = false;
            }
            MusicSystem.SetRepeatOneStatus(IsRepeatOne);

        }
        public void ChangeProgess(double progress)
        {
            if (Math.Abs(progress - MusicSystem.CurrentPlayer.CurrentTime) > 2.0)
            {
                MusicSystem.CurrentPlayer.CurrentTime = progress;
            }
            this.IsPlaying = MusicSystem.CurrentPlayer.Playing;
        }

        public void ChangeMusic(MusicInfo musicInfo)
        {
            this.CurrentMusic = musicInfo;
        }

        public void ChangeMusic(string title)
        {
            this.CurrentMusic = Musics.FirstOrDefault(c => c.Title == title);
        }

        private List<MusicInfo> musics;

        public List<MusicInfo> Musics
        {
            get
            {
                if (musics == null)
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

        private MusicInfo currentMusic;

        public MusicInfo CurrentMusic
        {
            get
            {
                if (currentMusic == null)
                {
                    InitCurrentMusic();
                }
                return currentMusic;
            }
            set
            {
                SetObservableProperty(ref currentMusic, value);
            }
        }

        private void InitCurrentMusic()
        {
            var musicIndex = int.Parse(SettingServer.Current.GetSetting(SettingServer.Properties.BreakPointMusicIndex, true));
            if (Musics.Count > 0)
            {
                if (musicIndex >= 0 && musicIndex <= Musics.Count - 1)
                {
                    CurrentMusic = Musics[musicIndex];
                }
                else
                {
                    CurrentMusic = Musics[0];
                }
            }
        }

        private bool isPlaying;

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {

                SetObservableProperty(ref isPlaying, value);

            }
        }

        private double currentTime;

        public double CurrentTime
        {
            get { return currentTime; }
            set
            {
                SetObservableProperty(ref currentTime, value);

            }
        }

        private double duration;

        public double Duration
        {
            get { return duration; }
            set
            {

                SetObservableProperty(ref duration, value);

            }
        }

        private MusicInfo previewMusic;

        public MusicInfo PreviewMusic
        {
            get
            {
                if (previewMusic == null)
                {
                    InitPreviewAndNextMusic();
                }
                return previewMusic;
            }
            set
            {
                SetObservableProperty(ref previewMusic, value);
            }
        }
        private MusicInfo nextMusic;

        public MusicInfo NextMusic
        {
            get
            {
                if (nextMusic == null)
                {
                    InitPreviewAndNextMusic();
                }
                return nextMusic;
            }
            set
            {
                SetObservableProperty(ref nextMusic, value);
            }
        }

        private bool isShuffle;

        public bool IsShuffle
        {
            get
            {
                return SettingServer.Current.GetSetting(SettingServer.Properties.IsShuffle);
            }
            set
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsShuffle, value);
                SetObservableProperty(ref isShuffle, value);

            }
        }


        private bool isRepeatOne;

        public bool IsRepeatOne
        {
            get
            {
                return SettingServer.Current.GetSetting(SettingServer.Properties.IsRepeatOne);
            }
            set
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsRepeatOne, value);
                SetObservableProperty(ref isRepeatOne, value);

            }
        }
        #endregion

        public RelayCommand PlayCommand { get; set; }

        public RelayCommand PreCommand { get; set; }

        public RelayCommand NextCommand { get; set; }

        public RelayCommand ShuffleCommand { get; set; }

        public RelayCommand RepeatOneCommand { get; set; }


    }
}

