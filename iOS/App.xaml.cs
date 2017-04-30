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
            App.Current.Resources["Bound"]  = (UIScreen.MainScreen.Bounds.Width).ToString();
			MainPage = App.MainMasterDetailPage;

            Messenger.Default.Register<string>(this, TokenHelper.WindowToken, HandleWindowResult);


        }

        private void HandleWindowResult(string obj)
        {
            Type pageType = Type.GetType("ProjectMato.iOS."+obj, false);
            if (pageType != null)
            {
                var pageObj = Activator.CreateInstance(pageType);
               
            
                MainMasterDetailPage.Detail = new NavigationPage(pageObj as Page);


            }
        }

        private static MasterDetailPage mainMasterDetailPage;
		public static MasterDetailPage MainMasterDetailPage {
			get {
				if (mainMasterDetailPage==null) {
					mainMasterDetailPage = new MasterDetailPage ();
					mainMasterDetailPage.Master = new MenuPage();
					mainMasterDetailPage.Detail = new NavigationPage(new NowPlayingPage ()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
				}
				return mainMasterDetailPage;
			}
			set	{  mainMasterDetailPage = value;}
		}
    }
}


