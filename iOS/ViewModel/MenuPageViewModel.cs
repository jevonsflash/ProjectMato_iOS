using GalaSoft.MvvmLight;
using System.Collections.Generic;
using ProjectMato.iOS.Model;
using Xamarin.Forms;

namespace ProjectMato.iOS.ViewModel
{
    public class MenuPageViewModel:ViewModelBase
    {
        public MenuPageViewModel()
        {
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
                        new MenuCellInfo() {Title = "正在播放" ,Code = "SearchPage",Icon = "Icon/search" },
                        new MenuCellInfo() {Title = "列队" ,Code = "SearchPage",Icon = "Icon/search" },
                        new MenuCellInfo() {Title = "音乐库" ,Code = "SearchPage",Icon = "Icon/search" },
                        new MenuCellInfo() {Title = "歌单" ,Code = "SearchPage",Icon = "Icon/search" },
                        new MenuCellInfo() {Title = "设置" ,Code = "SearchPage",Icon = "Icon/search" },
                    };
                }
                return _mainMenuCellInfos;
            }
            set { _mainMenuCellInfos = value; }
        }





    }




}