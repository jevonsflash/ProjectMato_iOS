using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using GalaSoft.MvvmLight;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS.ViewModel
{
    public class SettingPageViewModel : ViewModelBase
    {
        public SettingPageViewModel()
        {
            PropertyChanged += SettingPageViewModel_PropertyChanged;
            IsAutoLrc = SettingServer.Current.GetSetting(SettingServer.Properties.IsAutoLrc);
            IsAutoGA = SettingServer.Current.GetSetting(SettingServer.Properties.IsAutoGA);
            IsAutoLrc = SettingServer.Current.GetSetting(SettingServer.Properties.IsAutoLrc);
            this.SelectedBackgroundTable = BackgroundList.FirstOrDefault(c => c.IsSel = true);
        }

        //todo: 需要优化
        private void SettingPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedBackgroundTable))
            {
                for (var i = 0; i < this.BackgroundList.Count; i++)
                {
                    if (BackgroundList[i].IsSel == true)
                    {
                        BackgroundList[i].IsSel = false;
                        SettingServer.Current.SetSelectedBackground(BackgroundList[i]);
                    }
                    if (BackgroundList[i].BackgroundId == SelectedBackgroundTable.BackgroundId)
                    {
                        BackgroundList[i].IsSel = true;
                        SettingServer.Current.SetSelectedBackground(BackgroundList[i]);

                    }
                }
            }
            else if (e.PropertyName == nameof(IsAutoLrc))
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsAutoLrc, IsAutoLrc);

            }
            else if (e.PropertyName == nameof(IsAutoOffset))
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsAutoOffset, IsAutoOffset);

            }
            else if (e.PropertyName == nameof(IsAutoGA))
            {
                SettingServer.Current.SetSetting(SettingServer.Properties.IsAutoGA, IsAutoGA);

            }
        }



        #region Properties

        private bool _isAutoLrc;


        public bool IsAutoLrc
        {
            get
            {
                return _isAutoLrc;
            }
            set
            {
                _isAutoLrc = value;
                RaisePropertyChanged();

            }
        }
        private bool _isAutoOffset;


        public bool IsAutoOffset
        {
            get
            {
                return _isAutoOffset;
            }
            set
            {
                _isAutoOffset = value;
                RaisePropertyChanged();

            }
        }

        private bool _isAutoGA;

        public bool IsAutoGA
        {
            get
            {
                return _isAutoGA;
            }
            set
            {
                _isAutoGA = value;
                RaisePropertyChanged();

            }
        }


        private BackgroundTable _selectedBackgroundTable;

        public BackgroundTable SelectedBackgroundTable
        {
            get
            {
                if (_selectedBackgroundTable == null)
                {
                    _selectedBackgroundTable = new BackgroundTable();
                }
                return _selectedBackgroundTable;
            }
            set
            {
                _selectedBackgroundTable = value;
                RaisePropertyChanged();
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

                _backgroundList = value;
                RaisePropertyChanged();

            }
        }

        #endregion

    }
}
