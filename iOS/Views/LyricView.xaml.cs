using ProjectMato.iOS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;
using Xamarin.Forms;

namespace ProjectMato.iOS
{

    public partial class LyricView
    {

        public LrcInfo CurrentLrcItem { get; private set; }


        public LyricView()
        {
            this.InitializeComponent();
            this.BindingContext = MusicRelatedViewModel.Current;
            LBLyric.BindingContext = this.CurrentLrcItem;
            var musicRelatedViewModel = this.BindingContext as MusicRelatedViewModel;
            if (musicRelatedViewModel != null)
                musicRelatedViewModel.OnMusicChanged += Current_OnMusicChanged;
        }

        private void Current_OnMusicChanged(object sender, EventArgs e)
        {

            // 搜索API
            var json = MusicAPIServer.Search("执迷", 5);
            // 歌曲详情API
            json = MusicAPIServer.Detail("29775505", "300587");
            // 歌词API
            json = MusicAPIServer.Lyric("29775505");
            CurrentLrcItem = MusicAPIServer.ParseLrc(json);
            LBLyric.BindingContext = this.CurrentLrcItem;

        }

        public string ElapsedTime
        {
            get { return (string)GetValue(ElapsedTimeProperty); }
            set { SetValue(ElapsedTimeProperty, value); }
        }


        public static readonly BindableProperty ElapsedTimeProperty =
            BindableProperty.Create("ElapsedTime", typeof(string), typeof(LyricView), string.Empty, BindingMode.OneWay, null, OnElapsedTimeChanged);

        private static void OnElapsedTimeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            LyricView thisView = bindable as LyricView;

            if (thisView != null)
            {
                TimeSpan currentElapsedTime;
                TimeSpan.TryParse(thisView.ElapsedTime, out currentElapsedTime);

                var oldIndex = 0;

                var currentIndex = 0;
                for (var i = 0; i < thisView.CurrentLrcItem.LrcWords.Count; i++)
                {

                    if (thisView.CurrentLrcItem.LrcWords[i].IsCurrent)
                    {
                        oldIndex = i;
                    }

                    if (i == 0)
                    {
                        if (new TimeSpan(0, 0, 0, 0) < currentElapsedTime &&
                            thisView.CurrentLrcItem.LrcWords[i].Time > currentElapsedTime)
                        {
                            currentIndex = i;
                            break;
                        }
                    }
                    else
                    {
                        if (thisView.CurrentLrcItem.LrcWords[i - 1].Time < currentElapsedTime && thisView.CurrentLrcItem.LrcWords[i].Time > currentElapsedTime)
                        {
                            currentIndex = i;
                            break;
                        }
                    }
                }
                if (oldIndex != currentIndex)
                {
                    thisView.CurrentLrcItem.LrcWords[currentIndex].IsCurrent = true;
                    thisView.CurrentLrcItem.LrcWords[oldIndex].IsCurrent = false;
                }
            }
        }





        /// <summary>
        /// 初始化
        /// </summary>

        private static async Task<string> ReadLrcFile(string fileName)
        {
            string lrcStr = string.Empty;
            if (await FileHelper.IsExistFileAsync(fileName))
            {
                lrcStr = await FileHelper.ReadTxtFileAsync(fileName);
            }
            return lrcStr;
        }






    }

}
