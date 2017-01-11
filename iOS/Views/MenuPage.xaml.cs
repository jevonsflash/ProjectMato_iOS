using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProjectMato.iOS
{

    public class MenuTableView : TableView
    {
    }
    public partial class MenuPage : ContentPage
    {
        MasterDetailPage master;


        public MenuPage(MasterDetailPage m)
        {
            InitializeComponent();
			this.Queue.Host = this;
			this.Library.Host = this;
			this.NowPlaying.Host = this;
			this.Playlist.Host = this;
			this.Setting.Host = this;
			this.About.Host = this;
            master = m;

        }


        public void Selected(MenuCode item)
        {


            switch (item)
            {
                case MenuCode.NowPlaying:

                    var nowPlayingPage = new NavigationPage(new NowPlayingPage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
                    master.Detail = nowPlayingPage;
                    break;

                case MenuCode.Queue:

                    var queuePage = new NavigationPage(new QueuePage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
                    master.Detail = queuePage;
                    break;
                case MenuCode.Library:
                    var libraryPage = new NavigationPage(new LibraryPage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
                    master.Detail = libraryPage;
                    break;
                case MenuCode.Playlist:
                    var playlistPage = new NavigationPage(new PlaylistPage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
                    master.Detail = playlistPage;
                    break;

                case MenuCode.Setting:
                    var settingPage = new NavigationPage(new SettingPage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
                    master.Detail = settingPage;
                    break;
                case MenuCode.About:
                    var aboutPage = new NavigationPage(new AboutPage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White };
                    master.Detail = aboutPage;
                    break;
                default:
                    break;
            }
            master.IsPresented = false;  // close the slide-out
        }
    }

}
