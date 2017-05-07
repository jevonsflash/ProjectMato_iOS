using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.ViewModel;

namespace ProjectMato.iOS.Server
{

    public class SettingServer
    {
        public static class Properties
        {
            public const string IsNewSeason = "IsNewSeason";
            public const string IsSleepModeOn = "IsSleepModeOn";
            public const string TimingOffValue = "TimingOffValue";
            public const string IsStopWhenTerminate = "IsStopWhenTerminate";
            public const string IsAutoLrc = "IsAutoLrc";
            public const string IsAutoOffset = "IsAutoOffset";
            public const string IsAutoGA = "IsAutoGA";
            public const string BackgroundList = "BackgroundList";
            public const string IsShuffle = "IsShuffle";
            public const string IsRepeatOne = "IsRepeatOne";
            public const string IsRepeat = "IsRepeat";
            public const string BreakPointMusicIndex = "BreakPointMusicIndex";

        }
        private static SettingServer current;

        //private List<SettingTable> _settingTables; 
        /// <summary>
        /// 当前实例
        /// </summary>
        public static SettingServer Current
        {
            get
            {

                if (current == null)
                {
                    current = new SettingServer();
                }
                return current;
            }

        }

        private SettingServer()
        {
            DatabaseManager.Current.Connect();
        }

        ~SettingServer()
        {
            DatabaseManager.Current.Disconnect();
        }


        public void SetSetting(string key, bool value)
        {
            if (!GetAllSettings().Exists(c => c.Key == key))
            {
                InitSettingDefaultValue();
            }
            var obj = GetAllSettings()
                                   .FirstOrDefault(c => c.Key == key);
            if (obj != null)
            {
                obj.Value = value;
                DatabaseManager.Current.Update(obj);
            }
        }

        public void SetSetting(string key, string value)
        {
            if (!GetAllSettings().Exists(c => c.Key == key))
            {
                InitSettingDefaultValue();
            }
            var obj = GetAllSettings()
                                   .FirstOrDefault(c => c.Key == key);
            if (obj != null)
            {
                obj.StrValue = value;
                DatabaseManager.Current.Update(obj);
            }
        }

        public bool GetSetting(string key)
        {
            if (!GetAllSettings().Exists(c => c.Key == key))
            {
                InitSettingDefaultValue();
            }

            var obj =
                GetAllSettings()
                    .FirstOrDefault(c => c.Key == key);

            return obj != null && obj.Value;
        }

        public string GetSetting(string key, bool isStrValue)
        {
            if (!GetAllSettings().Exists(c => c.Key == key))
            {
                InitSettingDefaultValue();
            }

            var settings =
                GetAllSettings();
            var obj = settings.FirstOrDefault(c => c.Key == key);

            if (obj == null)
            {
                return string.Empty;

            }
            return obj.StrValue;

        }

        private void InitSettingDefaultValue()
        {
            DatabaseManager.Current.ClearSetting();

            DatabaseManager.Current.AddSettingTable(new SettingTable("睡眠模式开关", Properties.IsSleepModeOn, false));
            DatabaseManager.Current.AddSettingTable(new SettingTable("是否自动歌词", Properties.IsAutoLrc, false));
            DatabaseManager.Current.AddSettingTable(new SettingTable("是否自动滚动", Properties.IsAutoOffset, false));
            DatabaseManager.Current.AddSettingTable(new SettingTable("是否GA", Properties.IsAutoGA, false));
            DatabaseManager.Current.AddSettingTable(new SettingTable("离开后关闭", Properties.IsStopWhenTerminate, false));
            DatabaseManager.Current.AddSettingTable(new SettingTable("倒计时", Properties.TimingOffValue, "20"));
            DatabaseManager.Current.AddSettingTable(new SettingTable("歌曲上次播放位置", Properties.BreakPointMusicIndex, "0"));
            DatabaseManager.Current.AddSettingTable(new SettingTable("是否循环", Properties.IsRepeat, "0"));
            DatabaseManager.Current.AddSettingTable(new SettingTable("是否单曲循环", Properties.IsRepeatOne, "0"));
            DatabaseManager.Current.AddSettingTable(new SettingTable("是否随机播放", Properties.IsShuffle, "0"));
        }

        private void InitBackgroundDefaultValue()
        {

            DatabaseManager.Current.ClearBackground();

            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("绿茵", "Grass", "Img/Linear1.png", false, "#93ad34", "#198a62", "#EFF5D8"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("清蓝", "Grass", "Img/Linear2.png", false, "#31A7D4", "#5648c1", "#EFEDFF"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("亮紫", "Grass", "Img/Linear3.png", false, "#2b85a6", "#7b4397", "#FCF7FF"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("沼泽", "Grass", "Img/Linear4.png", false, "#248556", "#16226e", "#F1FFF8"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("赤岩", "Grass", "Img/Linear5.png", false, "#d16645", "#13052e", "#FFF4F1"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("番茄", "Grass", "Img/Linear6.png", false, "#dca74a", "#d23c39", "#FFF3F3"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("绿茵", "Grass", "Img/Linear7.png", false, "#7f7280", "#000000", "#EAEAEA"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("绿茵", "Grass", "Img/Linear8.png", false, "#49976D", "#0c2b50", "#EFF6FF"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("绿茵", "Grass", "Img/Linear9.png", true, "#29c0db", "#1178c2", "#E9F6FF"));


        }

        private string GetColorFromAppconfig(string key)
        {

            var result = "";
            result = App.Current.Resources[key] as string;
            return result;
        }

        public List<SettingTable> GetAllSettings()
        {
            //if (_settingTables==null)
            //{
            //    _settingTables = DatabaseManager.Current.FetchSettingTables();
            //}

            return DatabaseManager.Current.FetchSettingTables();

        }

        public List<BackgroundTable> GetAllBackgrounds()
        {
            var result = DatabaseManager.Current.FetchBackgroundItems();
            if (result.Count == 0)
            {
                InitBackgroundDefaultValue();
                result = DatabaseManager.Current.FetchBackgroundItems();
            }
            return result;


        }

        public BackgroundTable GetSelectedBackground()
        {
            var result = DatabaseManager.Current.QuerySelectedBackground();
            if (result == null)
            {
                result = GetAllBackgrounds().FirstOrDefault(c => c.IsSel);
            }
            return result;
        }

        public void SetSelectedBackground(BackgroundTable background)
        {
            DatabaseManager.Current.Update(background);
        }
    }
}
