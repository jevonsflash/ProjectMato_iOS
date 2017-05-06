using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MediaPlayer;
using AVFoundation;
using Foundation;
using ProjectMato.iOS;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Helper;
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


        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            //CommonHelper.GoPage("NowPlayingPage");

        }

        private void MusicItemView_OnOnJumptoOtherPage(object sender, MusicFunctionEventArgs e)
        {
            if (e.MenuCellInfo.Code == "Delete")
            {
                var musicInfo = e.MusicInfo;
                var queuePageViewModel = this.BindingContext as QueuePageViewModel;
                if (queuePageViewModel != null)
                    queuePageViewModel.DeleteAction(musicInfo);
            }
        }

    }
}

