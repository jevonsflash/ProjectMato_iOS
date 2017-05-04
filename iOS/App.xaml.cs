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
            App.Current.Resources["Bound"] = (UIScreen.MainScreen.Bounds.Width).ToString();
            MainPage = App.MainMasterDetailPage;

            Messenger.Default.Register<WindowArg>(this, TokenHelper.WindowToken, HandleWindowResult);


        }

        private async void HandleWindowResult(WindowArg obj)
        {
            if (obj.IsNavigate)
            {
                await MainMasterDetailPage.Detail.Navigation.PushAsync(GetPageInstance(obj.Name, obj.Args));
            }
            else
            {
                MainMasterDetailPage.Detail = GetPageInstance(obj.Name, obj.Args);

            }
        }



        private static Page GetPageInstance(string obj, object[] args)
        {
            Page result = null;
            Type pageType = Type.GetType(typeof(App).Namespace + "." + obj, false);
            if (pageType != null)
            {
                var pageObj = Activator.CreateInstance(pageType, args);
                result = new NavigationPage(pageObj as Page) { BarBackgroundColor = (Color)Current.Resources["PhoneForegroundBrush"], BarTextColor = (Color)Current.Resources["PhoneContrastForegroundBrush"] };
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
                    mainMasterDetailPage.Detail = GetPageInstance("NowPlayingPage", null);
                }
                return mainMasterDetailPage;
            }
            set { mainMasterDetailPage = value; }
        }
    }
}


