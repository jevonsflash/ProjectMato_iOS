using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class SettingPageViewModel : BaseViewModel
    {
        public SettingPageViewModel()
        {
            PropertyChanged += SettingPageViewModel_PropertyChanged;
        }

        private void SettingPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SettingServer.Properties.SelectedBackground)
            {
                SettingServer.Current.SetSelectedBackground(this.SelectedBackground);
            }

        }



        #region Properties




        public bool IsAutoLrc
        {
            get
            {
                return SettingServer.Current.GetSetting(SettingServer.Properties.IsAutoLrc);

            }
            set
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsAutoLrc, value);

            }
        }
        public bool IsAutoOffset
        {
            get
            {
                return SettingServer.Current.GetSetting(SettingServer.Properties.IsAutoOffset);

            }
            set
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsAutoOffset, value);

            }
        }

        public bool IsAutoGA
        {
            get
            {
                return SettingServer.Current.GetSetting(SettingServer.Properties.IsAutoGA);

            }
            set
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsAutoGA, value);

            }
        }



        public List<BackgroundTable> BackgroundList
        {
            get
            {
                var result= SettingServer.Current.GetAllBackgrounds();
                return result;
                
            }
        }

        private BackgroundTable _selectedBackground;

        public BackgroundTable SelectedBackground
        {
            get
            {
                if (_selectedBackground == null)
                {
                    _selectedBackground = SettingServer.Current.GetSelectedBackground();
                }
                return _selectedBackground;
            }
            set
            {
                base.SetObservableProperty(ref _selectedBackground, value);
            }
        }

        #endregion

    }
}
