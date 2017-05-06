using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.Server;
using Xamarin.Forms;

namespace ProjectMato.iOS.Controls
{
    public partial class MusicMiniView : ContentView
    {
        public MusicMiniView()
        {
            InitializeComponent();
            this.BindingContext = MusicRelatedViewModel.Current;
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            CommonHelper.GoPage("NowPlayingPage");
        }
    }
}
