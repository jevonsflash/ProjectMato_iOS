using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;
using UIKit;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistChoosePage
    {
        public event EventHandler<PlaylistTable> OnFinished; 
        public PlaylistChoosePage()
        {
            InitializeComponent();
            this.BindingContext = MusicInfoServer.Current.GetPlaylist();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            PlaylistTable playlist;
            if (e.SelectedItem!=null)
            {
                playlist = e.SelectedItem as PlaylistTable;
                this.OnFinished?.Invoke(this, playlist);

            }
           
        }

    }
}
