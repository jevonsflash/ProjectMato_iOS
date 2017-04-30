using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using ProjectMato.iOS.Helper;
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

            Messenger.Default.Register<string>(this, TokenHelper.WindowToken, HandleWindowResult);


        }

        private void HandleWindowResult(string obj)
        {
            MainMasterDetailPage.Detail = GetPageInstance(obj);
        }

        private static Page GetPageInstance(string obj)
        {
            Page result = null;
            Type pageType = Type.GetType("ProjectMato.iOS." + obj, false);
            if (pageType != null)
            {
                var pageObj = Activator.CreateInstance(pageType);


                result = new NavigationPage(pageObj as Page);


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
                    mainMasterDetailPage.Detail = GetPageInstance("NowPlayingPage");
                }
                return mainMasterDetailPage;
            }
            set { mainMasterDetailPage = value; }
        }
    }
}


