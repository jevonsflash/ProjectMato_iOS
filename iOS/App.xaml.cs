using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.Server;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectMato.iOS
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            var currentSkin = SettingServer.Current.GetSelectedBackground();
            CommonHelper.SetTheme(currentSkin);
            App.Current.Resources["Bound"] = (UIScreen.MainScreen.Bounds.Width).ToString();
            MainPage = App.MainMasterDetailPage;
            Messenger.Default.Register<WindowArg>(this, TokenHelper.WindowToken, HandleWindowResult);


        }

        private async void HandleWindowResult(WindowArg obj)
        {
            var barBackgroundColor = (Color)Current.Resources["PhoneForegroundBrush"];
            var barTextColor = (Color)Current.Resources["PhoneContrastForegroundBrush"];
            if (obj.IsNavigate)
            {
                await MainMasterDetailPage.Detail.Navigation.PushAsync(GetPageInstance(obj.Name, obj.Args, barTextColor, barBackgroundColor));
            }
            else
            {
                if (obj.Name == "NowPlayingPage")
                {
                    var barItem = (new ToolbarItem("当前列队", "Icon/queue", () =>
                    {
                        CommonHelper.GoPage("QueuePage");
                    }));
                    MainMasterDetailPage.Detail = GetPageInstance(obj.Name, obj.Args, barBackgroundColor, barTextColor, barItem: barItem);

                }
                else
                {
                    var barItem = (new ToolbarItem("正在播放", "Icon/nowplaying", () =>
                    {
                        CommonHelper.GoPage("NowPlayingPage");
                    }));

                    MainMasterDetailPage.Detail = GetPageInstance(obj.Name, obj.Args, barBackgroundColor, barTextColor, barItem: barItem);
                }
                MainMasterDetailPage.IsPresented = false;
            }
        }



        private static NavigationPage GetPageInstance(string obj, object[] args, Color barBackgroundColor, Color barTextColor, bool isEnableTranslucentNavigationBar = false, ToolbarItem barItem = null)
        {
            NavigationPage result = null;
            Type pageType = Type.GetType(typeof(App).Namespace + "." + obj, false);
            if (pageType != null)
            {
                var pageObj = Activator.CreateInstance(pageType, args) as Page;
                if (barItem != null) pageObj.ToolbarItems.Add(barItem);
                result = new NavigationPage(pageObj)
                {
                    BarBackgroundColor = barBackgroundColor,
                    BarTextColor = barTextColor,
                };
                if (isEnableTranslucentNavigationBar)
                {
                    Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.EnableTranslucentNavigationBar(result
    .On<Xamarin.Forms.PlatformConfiguration.iOS>());

                }
            }
            return result;
        }



        private static MasterDetailPage mainMasterDetailPage;
        public static MasterDetailPage MainMasterDetailPage
        {
            get
            {
                if (mainMasterDetailPage == null)
                {
                    mainMasterDetailPage = new MasterDetailPage();
                    mainMasterDetailPage.Master = new MenuPage();
                    mainMasterDetailPage.Detail = GetPageInstance("NowPlayingPage", null, (Color)Current.Resources["PhoneForegroundBrush"], (Color)Current.Resources["PhoneContrastForegroundBrush"]);
                }
                return mainMasterDetailPage;
            }
            set { mainMasterDetailPage = value; }
        }
    }
}


