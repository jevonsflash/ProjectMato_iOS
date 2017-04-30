using GalaSoft.MvvmLight;
using System.Collections.Generic;
using ProjectMato.iOS.Model;
using Xamarin.Forms;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using ProjectMato.iOS.Helper;

namespace ProjectMato.iOS.ViewModel
{
    public class MenuPageViewModel : ViewModelBase
    {
        public MenuPageViewModel()
        {
            this.PropertyChanged += MenuPageViewModel_PropertyChanged;
        }

        private void MenuPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentMenuCellInfo))
            {
                CommonHelper.GoPage(CurrentMenuCellInfo.Code);
            }
        }

        private List<MenuCellInfo> _mainMenuCellInfos;

        public List<MenuCellInfo> MainMenuCellInfos
        {
            get
            {
                if (_mainMenuCellInfos == null)
                {
                    _mainMenuCellInfos = new List<MenuCellInfo>()
                    {
                        new MenuCellInfo() {Title = "搜索" ,Code = "SearchPage",Icon = "Icon/search" },
                        new MenuCellInfo() {Title = "正在播放" ,Code = "NowPlayingPage",Icon = "Icon/headphone" },
                        new MenuCellInfo() {Title = "列队" ,Code = "QueuePage",Icon = "Icon/queue2" },
                        new MenuCellInfo() {Title = "音乐库" ,Code = "LibraryPage",Icon = "Icon/folder" },
                        new MenuCellInfo() {Title = "歌单" ,Code = "PlaylistPage",Icon = "Icon/playlist" },
                        new MenuCellInfo() {Title = "设置" ,Code = "SettingPage",Icon = "Icon/setting" },
                    };
                }
                return _mainMenuCellInfos;
            }
            set { _mainMenuCellInfos = value; }
        }

        private MenuCellInfo _currentMenuCellInfo;

        public MenuCellInfo CurrentMenuCellInfo
        {
            get
            {
                if (_currentMenuCellInfo == null)
                {
                    _currentMenuCellInfo = MainMenuCellInfos[1];
                }
                return _currentMenuCellInfo;
            }
            set
            {
                _currentMenuCellInfo = value;
                base.RaisePropertyChanged();
            }
        }




    }




}