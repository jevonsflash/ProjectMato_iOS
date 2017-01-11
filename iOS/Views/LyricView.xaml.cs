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
        private string elapsedtime = string.Empty;
        private string songname = string.Empty;
        private string artistname = string.Empty;
        private FileHelper FileHelper = new FileHelper();
        private List<Result2> list;

        public LRCItem lrcItem { get; private set; }


        public LyricView()
        {
            this.InitializeComponent();
            
        }




        public string ElapsedTime
        {
            get { return (string)GetValue(ElapsedTimeProperty); }
            set { SetValue(ElapsedTimeProperty, value); }
        }


        // Using a DependencyProperty as the backing store for ElapsedTime.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty ElapsedTimeProperty =
            BindableProperty.Create("ElapsedTime", typeof(string), typeof(LyricView), string.Empty, BindingMode.OneWay, null, new BindableProperty.BindingPropertyChangedDelegate(OnElapsedTimeChanged));

        private static void OnElapsedTimeChanged(BindableObject bindable, object oldValue, object newValue)
        {

            LyricView testControl = bindable as LyricView;
            testControl.elapsedtime = testControl.ElapsedTime;
            if (testControl.LBLyric.BindingContext is List<LrcWord>)
            {


                var lrcWords = testControl.LBLyric.BindingContext as List<LrcWord>;
                var item = lrcWords.FirstOrDefault(c => c.Time.ToString().Contains(testControl.elapsedtime));
                if (item != null)
                {
                    int currentIndex = lrcWords.ToList().IndexOf(item);
                    lrcWords[currentIndex].IsCurrent = true;
                    //todo:设置画刷
                    if (SettingServer.Current.GetSetting(SettingServer.Properties.IsAutoOffset))
                    {
                        //todo:设置滚动
                    }

                }
            }

        }


        #region 请求歌词算法
        /// <summary>
        /// 发送歌词请求
        /// </summary>
        public void DoHttpWebRequest(string keyWord)
        {
            HttpHelper ht = new HttpHelper();
            string url = "http://geci.me/api/lyric/" + keyWord;
            ht.CreatePostHttpResponse(url);
            ht.FileWatchEvent += ht_FileWatchEvent;
        }
        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ht_FileWatchEvent(object sender, CompleteEventArgs e)
        {
            list = LRCSer.GecimeLyricDeserializer(e.Node).Result.ToList();
            if (list.Count == 0)
            {
                ShowErr();
            }
            else
            {

                Result2 result2 = list.First();
                HttpHelper ht = new HttpHelper();
                ht.CreatePostHttpResponse(result2.lrc);
                ht.FileWatchEvent += ht_FileWatchEvent2;
            }
        }

        private void ShowErr()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 处理返回值2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void ht_FileWatchEvent2(object sender, CompleteEventArgs e)
        {
            string lrcStr = e.Node;
            string fileName = songname + "-" + artistname + ".lrc";
            if (await FileHelper.CreateAndWriteFileAsync(fileName, lrcStr))
            {
                lrcItem = LRCSer.InitLrc(lrcStr);




            }


        }


        #endregion
        /// <summary>
        /// 初始化
        /// </summary>
        public async void InitializeLrc()
        {
            string fileName = "MatoLrc\\" + songname + "-" + artistname + ".lrc";
            string lrcStr;

            lrcStr = await ReadLrcFile(fileName);
            if (!string.IsNullOrEmpty(lrcStr))
            {
                lrcItem = LRCSer.InitLrc(lrcStr);

            }
            else
            {
                if (SettingServer.Current.GetSetting(SettingServer.Properties.IsAutoLrc))
                {
                    DoHttpWebRequest(songname);
                }
            }
        }

        private static async Task<string> ReadLrcFile(string fileName)
        {
            string lrcStr = string.Empty;
            if (await FileHelper.IsExistFileAsync(fileName))
            {
                lrcStr = await FileHelper.ReadTxtFileAsync(fileName);
            }
            return lrcStr;
        }

        private void UpdateMusic()
        {

            HideErr();
            return;

        }

        private void HideErr()
        {

        }




    }

}
