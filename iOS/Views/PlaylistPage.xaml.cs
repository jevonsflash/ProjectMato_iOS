using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistPage : ContentPage
    {
        public PlaylistPage()
        {
            InitializeComponent();
            this.BindingContext=new PlaylistPageViewModel();
        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var playlist = e.SelectedItem as PlaylistTable;

            await Navigation.PushAsync(new PlaylistEntryPage(playlist));
        }
    }
}
