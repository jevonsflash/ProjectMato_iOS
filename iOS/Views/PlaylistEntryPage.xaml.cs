using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistEntryPage : ContentPage
    {
        public PlaylistEntryPage(PlaylistTable playlistTable)
        {

            InitializeComponent();
            this.BindingContext=new PlaylistEntryPageViewModel(playlistTable);
        }
    }
}
