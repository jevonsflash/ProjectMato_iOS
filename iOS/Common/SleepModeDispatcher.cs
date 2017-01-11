using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using CoreFoundation;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.Common
{

    public class SleepModeDispatcher
    {
        private Timer timer = new Timer();

        public Timer Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        public SleepModeDispatcher()
        {
            var defaultInterval = new TimeSpan(0, 20, 0).TotalSeconds;
            SettingServer.Current.SetSetting(SettingServer.Properties.TimingOffValue, defaultInterval.ToString());
            SettingServer.Current.SetSetting(SettingServer.Properties.IsSleepModeOn, false);
        }

        public void SleepModeSet()
        {
            Timer.Interval = double.Parse(SettingServer.Current.GetSetting(SettingServer.Properties.TimingOffValue, true));

            Timer.Elapsed += Timer_Tick1;
        }

        public void SleepModeUnSet()
        {
            Timer.Elapsed -= Timer_Tick1;
        }

        private void Timer_Tick1(object sender, object e)
        {
            if (SettingServer.Current.GetSetting(SettingServer.Properties.IsStopWhenTerminate))
            {
                //暂停播放
                MusicSystem.Stop();
            }

            Timer.Elapsed -= Timer_Tick1;
            
            //ios客户端无法自动退出app
            //Application.Current.Exit();
        }

        public void SleepModeOn()
        {
            Timer.Start();
        }
        public void SleepModeOff()
        {
            Timer.Stop();
        }
    }

}
