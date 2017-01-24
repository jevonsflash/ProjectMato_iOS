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
        private NavigationPage detailPage;

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

        }

        void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            var musicRelatedViewModel = (this.BindingContext) as MusicRelatedViewModel;
            if (musicRelatedViewModel != null)
                musicRelatedViewModel.ChangeProgess(e.NewValue);
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            this.detailPage = new NavigationPage(new QueuePage()) { BarBackgroundColor = Color.Black, BarTextColor = Color.White }; ;
            App.MainMasterDetailPage.Detail = this.detailPage;
        }
    }
}

