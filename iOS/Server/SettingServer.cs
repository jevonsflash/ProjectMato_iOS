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
            public const string SelectedBackground = "SelectedBackground";
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

            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("绿茵", "Grass", "Img/Grass.png", false, "0x091000|0x122000"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("伞", "Umbrella", "Img/Umbrella.png", false, "0x2f1f01|0x462e01"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("电吉他", "Guitar", "Img/Guitar.jpg", false, "0x060829|0x090e44"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("女孩", "Girl", "Img/Girl.jpg", false, "0x241418|0x3f232a"));

            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("平静", "Peace", "Img/Peace.png", false, "0x01102f|0x01194b"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("地下铁", "Metro", "Img/Metro.png", false, "0x000000|0x2d2d2d"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("紫罗兰", "Violet", "Img/Violet.png", false, "0x1d082d|0x2d0d47"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("童话", "Fairyland", "Img/Fairyland.png", false, "0x14262d|0x213e4a"));
            DatabaseManager.Current.AddBackgroundEntry(new BackgroundTable("葡萄酒", "Wine", "Img/Wine.png", true, "0x2d1d20|0x4a2f35"));

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
            return DatabaseManager.Current.LookupSelectedBackground();

        }

        public void SetSelectedBackground(BackgroundTable background)
        {

            var oldObj = GetSelectedBackground();
            oldObj.IsSel = false;

            var newObj = GetAllBackgrounds().FirstOrDefault(c => c.BackgroundId == background.BackgroundId);
            newObj.IsSel = true;
            DatabaseManager.Current.Update(oldObj);
            DatabaseManager.Current.Update(newObj);
        }

        public void SetSelectedBackground(string name)
        {

            var oldObj = GetSelectedBackground();
            oldObj.IsSel = false;

            var newObj = GetAllBackgrounds().FirstOrDefault(c => c.Name == name);
            newObj.IsSel = true;
            DatabaseManager.Current.Update(oldObj);
            DatabaseManager.Current.Update(newObj);
        }


    }
}
