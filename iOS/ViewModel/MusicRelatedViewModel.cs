using AVFoundation;
using Foundation;
using ProjectMato.iOS.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS
{
    public class MusicRelatedViewModel : ViewModelBase
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
            public const string IsRepeatOne = "IsRepeatOne";
            public const string IsShuffle = "IsShuffle";
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
            this.IsRepeatOne = SettingServer.Current.GetSetting(SettingServer.Properties.IsRepeatOne);
            this.IsShuffle = SettingServer.Current.GetSetting(SettingServer.Properties.IsShuffle);
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

            else if (e.PropertyName == Properties.IsShuffle)
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsShuffle, this.IsShuffle);
            }

            else if (e.PropertyName == Properties.IsRepeatOne)
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsRepeatOne, this.IsRepeatOne);
                MusicSystem.SetRepeatOneStatus(this.IsRepeatOne);
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
            this.IsPlaying = !this.IsPlaying;
        }

        private void ShuffleAction(object obj)
        {
            this.IsShuffle = !this.IsShuffle;
            if (IsShuffle)
            {
                MusicSystem.UpdateShuffleMap();
            }

        }

        private void RepeatOneAction(object obj)
        {
            this.IsRepeatOne = !this.IsRepeatOne;

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

        private List<MusicInfo> _musics;

        public List<MusicInfo> Musics
        {
            get
            {
                if (_musics == null)
                {
                    _musics = MusicSystem.MusicInfos;
                }

                return _musics;
            }
            set
            {
                _musics = value;
                RaisePropertyChanged();
            }
        }

        private MusicInfo _currentMusic;

        public MusicInfo CurrentMusic
        {
            get
            {
                if (_currentMusic == null)
                {
                    InitCurrentMusic();
                }
                return _currentMusic;
            }
            set
            {
                _currentMusic = value;
                RaisePropertyChanged();
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

        private bool _isPlaying;

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;

                    RaisePropertyChanged();

                }
            }
        }

        private double _currentTime;

        public double CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                RaisePropertyChanged();
            }
        }

        private double _duration;

        public double Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;

                RaisePropertyChanged();
            }
        }

        private MusicInfo _previewMusic;

        public MusicInfo PreviewMusic
        {
            get
            {
                if (_previewMusic == null)
                {
                    InitPreviewAndNextMusic();
                }
                return _previewMusic;
            }
            set
            {
                _previewMusic = value;
                RaisePropertyChanged();
            }
        }
        private MusicInfo _nextMusic;

        public MusicInfo NextMusic
        {
            get
            {
                if (_nextMusic == null)
                {
                    InitPreviewAndNextMusic();
                }
                return _nextMusic;
            }
            set
            {
                _nextMusic = value;
                RaisePropertyChanged();
            }
        }

        private bool _isShuffle;

        public bool IsShuffle
        {
            get
            {
                return _isShuffle;
            }
            set
            {
                _isShuffle = value;
                RaisePropertyChanged();
            }
        }


        private bool _isRepeatOne;

        public bool IsRepeatOne
        {
            get
            {
                return _isRepeatOne;
            }
            set
            {
                _isRepeatOne = value;
                RaisePropertyChanged();
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

