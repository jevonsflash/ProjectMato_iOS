using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistEntryPage : ContentPage
    {
        public PlaylistEntryPage(PlaylistInfo playlist)
        {

            InitializeComponent();
            this.BindingContext=new PlaylistEntryPageViewModel(playlist);
        }

        private async void MusicItemView_OnOnJumptoOtherPage(object sender, MusicFunctionEventArgs e)
        {
            if (e.FunctionType == MusicFunctionType.GoAlbumPage)
            {
                var albumInfo = MusicInfoServer.Current.GetAlbumInfos().Find(c => c.Title == e.MusicInfo.AlbumTitle);
                await Navigation.PushAsync(new AlbumPage(albumInfo));

            }
            else if (e.FunctionType == MusicFunctionType.GoArtistPage)
            {
                var artistInfo = MusicInfoServer.Current.GetArtistInfos().Find(c => c.Title == e.MusicInfo.Artist);
                await Navigation.PushAsync(new ArtistPage(artistInfo));
            }
        }
    }
}
