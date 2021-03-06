﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class SleepModePageViewModel : ViewModelBase
    {
        public SleepModePageViewModel()
        {
            this.IsSleepModeOn = _isSleepModeOn = SettingServer.Current.GetSetting(SettingServer.Properties.IsSleepModeOn);
            this.PropertyChanged += SleepModePageViewModel_PropertyChanged;
            this.TimingOffValue= double.Parse(SettingServer.Current.GetSetting(SettingServer.Properties.TimingOffValue, true));

        }

        private void SleepModePageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName==SettingServer.Properties.IsSleepModeOn)
            {
                 SettingServer.Current.SetSetting(SettingServer.Properties.IsSleepModeOn, IsSleepModeOn);
     
            }
            else if (e.PropertyName==SettingServer.Properties.TimingOffValue)
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.TimingOffValue, TimingOffValue.ToString());

            }
        }

        private bool _isSleepModeOn;
        public bool IsSleepModeOn
        {
            get
            {
                return _isSleepModeOn;
            }
            set
            {
                _isSleepModeOn = value;
                base.RaisePropertyChanged();
            }
        }

        private double _timingOffValue;

        public double TimingOffValue
        {
            get { return _timingOffValue; }
            set
            {
                _timingOffValue = value;
                base.RaisePropertyChanged();
            }
        }


        public bool IsStopWhenTerminate
        {
            get
            {
                return SettingServer.Current.GetSetting(SettingServer.Properties.IsStopWhenTerminate);

            }
            set
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsStopWhenTerminate, value);

            }
        }
    }
}
