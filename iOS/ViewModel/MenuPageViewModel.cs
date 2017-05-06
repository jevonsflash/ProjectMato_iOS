using GalaSoft.MvvmLight;
using System.Collections.Generic;
using ProjectMato.iOS.Model;
using Xamarin.Forms;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Helper;

namespace ProjectMato.iOS.ViewModel
{
    public class MenuPageViewModel : ViewModelBase
    {
        private static MenuPageViewModel current;

        public static MenuPageViewModel Current
        {
            get
            {
                if (current == null)
                {
                    current = new MenuPageViewModel();
                }
                return current;
            }

        }

        private MenuPageViewModel()
        {
            this.PropertyChanged += MenuPageViewModel_PropertyChanged;
        }

        private void MenuPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentMenuCellInfo))
            {
                Messenger.Default.Send<WindowArg>(new WindowArg(CurrentMenuCellInfo.Code), TokenHelper.WindowToken);
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
                if (_currentMenuCellInfo != value)
                {
                    _currentMenuCellInfo = value;
                    base.RaisePropertyChanged();
                }

            }
        }




    }




}