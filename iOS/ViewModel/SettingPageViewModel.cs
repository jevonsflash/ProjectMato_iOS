using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class SettingPageViewModel : BaseViewModel
    {
        public SettingPageViewModel()
        {
            PropertyChanged += SettingPageViewModel_PropertyChanged;
            BackgroundList.CollectionChanged += BackgroundList_CollectionChanged;
        }

        private void BackgroundList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action==NotifyCollectionChangedAction.Replace)
            {
                SettingServer.Current.SetSelectedBackground(e.NewItems[0] as BackgroundTable);
            }
        }

        //todo: 需要优化
        private void SettingPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

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


        private ObservableCollectionEx<BackgroundTable> _backgroundList;


        public ObservableCollectionEx<BackgroundTable> BackgroundList
        {
            get
            {
                if (_backgroundList == null)
                {
                    _backgroundList = new ObservableCollectionEx<BackgroundTable>(SettingServer.Current.GetAllBackgrounds());
                }
                return _backgroundList;
            }
            set
            {


                SetObservableProperty(ref _backgroundList, value);
            }
        }

        #endregion

    }
}
