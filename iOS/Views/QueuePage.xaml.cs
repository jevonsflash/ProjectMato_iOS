using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MediaPlayer;
using AVFoundation;
using Foundation;
using ProjectMato.iOS;
using ProjectMato.iOS.Server;
using ProjectMato.iOS.ViewModel;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS
{
    public partial class QueuePage : ContentPage
    {
        private NavigationPage detailPage;
        public QueuePage()
        {
            // NSTimer nstimer = new NSTimer(new NSDate(), new TimeSpan(0, 0, 3), new Action<NSTimer>(DoUpdate), true);
            InitializeComponent();
            this.BindingContext = new QueuePageViewModel();

        }


        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            this.detailPage = new NavigationPage(new NowPlayingPage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White }; 
            App.MainMasterDetailPage.Detail = this.detailPage;

        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var musicInfo = (button.BindingContext as MusicInfo);
                var queuePageViewModel = this.BindingContext as QueuePageViewModel;
                if (queuePageViewModel != null)
                    queuePageViewModel.DeleteAction(musicInfo);
            }
        }

        private async void MusicItemView_OnOnJumptoOtherPage(object sender, MusicFunctionEventArgs e)
        {
            //if (e.FunctionType == MusicFunctionType.GoAlbumPage)
            //{
            //    var albumInfo = MusicInfoServer.Current.GetAlbumInfos().Find(c => c.Title == e.MusicInfo.AlbumTitle);
            //    await Navigation.PushAsync(new AlbumPage(albumInfo));

            //}
            //else if (e.FunctionType == MusicFunctionType.GoArtistPage)
            //{
            //    var artistInfo = MusicInfoServer.Current.GetArtistInfos().Find(c => c.Title == e.MusicInfo.Artist);
            //    await Navigation.PushAsync(new ArtistPage(artistInfo));
            //}

            //else if (e.FunctionType == MusicFunctionType.Delete)
            //{
            //    var musicInfo = e.MusicInfo;
            //    var queuePageViewModel = this.BindingContext as QueuePageViewModel;
            //    if (queuePageViewModel != null)
            //        queuePageViewModel.DeleteAction(musicInfo);
            //}
        }

    }
}

