using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }
		private static MasterDetailPage mainMasterDetailPage;
		public static MasterDetailPage MainMasterDetailPage {
			get {
				if (mainMasterDetailPage==null) {
					mainMasterDetailPage = new MasterDetailPage ();
					mainMasterDetailPage.Master = new MenuPage (mainMasterDetailPage);
					mainMasterDetailPage.Detail = new NavigationPage(new NowPlayingPage ()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
				}
				return mainMasterDetailPage;
			}
			set	{  mainMasterDetailPage = value;}
		}

		public static Color NavTint {
			get {
				return Color.FromHex ("3498DB"); // Xamarin Blue
			}
		}
		public static Color MenuBackgroundColor {
			get {
				return Color.FromHex ("2C3E50"); // Xamarin DarkBlue
			}
		}
    }
}


