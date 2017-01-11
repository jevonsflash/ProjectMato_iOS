using System;
using System.Collections.Generic;
using ProjectMato.iOS.Controls;
using UIKit;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs.Enums;

namespace ProjectMato.iOS
{
    public partial class NowPlayingPage : ContentPage
    {
        public NowPlayingPage()
        {

            InitializeComponent();
            this.BindingContext = MusicRelatedViewModel.Current;
            MusicRelatedViewModel.Current.OnMusicChanged += Current_OnMusicChanged;
            var button = new ImageButton()
            {

            };
            InitFliper();
        }

        private void Current_OnMusicChanged(object sender, EventArgs e)
        {
            InitFliper();
        }

        private void InitFliper()
        {
            this.MusicFliperCarouselView.IsFlipEnable = false;
            this.MusicFliperCarouselView.AnimateScroll = false;
            this.MusicFliperCarouselView.Position = new Point(375, 0);
            this.MusicFliperCarouselView.AnimateScroll = true;
            this.MusicFliperCarouselView.IsFlipEnable = true;

        }

        void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            var musicRelatedViewModel = (this.BindingContext) as MusicRelatedViewModel;
            if (musicRelatedViewModel != null)
                musicRelatedViewModel.ChangeProgess(e.NewValue);
        }


        private void MusicFliperCarouselView_OnOnFlipped(object sender, OnFlippedEventArgs e)
        {
            var musicRelatedViewModel = (this.BindingContext) as MusicRelatedViewModel;
            if (musicRelatedViewModel != null)
                if (e.FlipType == FlipType.前进)
                {
                    musicRelatedViewModel.NextAction(null);

                }
                else
                {
                    musicRelatedViewModel.PreAction(null);
                }
        }
    }
}

