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




        private void IsFavouriteButton_OnClicked(object sender, EventArgs e)
        {
            var imageButton = sender as ImageButton;
            var musicInfo = imageButton.BindingContext as MusicInfo;
            if (musicInfo != null)
            {
                if (!musicInfo.IsFavourite)
                {

                    if (MusicInfoServer.Current.CreatePlaylistEntryToMyFavourite(musicInfo))
                    {
                        musicInfo.IsFavourite = !musicInfo.IsFavourite;
                    }

                }
                else
                {
                    if (MusicInfoServer.Current.DeletePlaylistEntryFromMyFavourite(musicInfo))
                    {
                        musicInfo.IsFavourite = !musicInfo.IsFavourite;
                    }

                }
            }
        }


        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            this.detailPage = new NavigationPage(new NowPlayingPage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White }; ;
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
    }
}

