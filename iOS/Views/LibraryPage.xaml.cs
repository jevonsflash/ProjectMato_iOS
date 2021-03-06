﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.Server;
using Xamarin.Forms;
using XLabs;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS
{
    public partial class LibraryPage
    {

        public LibraryPage()
        {
            InitializeComponent();
            this.BindingContext = new LibraryPageViewModel();

        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is ArtistInfo)
            {
                var artistInfo = e.SelectedItem as ArtistInfo;
                CommonHelper.GoNavigate("ArtistPage", new object[] { artistInfo });

            }
            else if (e.SelectedItem is AlbumInfo)
            {
                var albumInfo = e.SelectedItem as AlbumInfo;
                CommonHelper.GoNavigate("AlbumPage", new object[] { albumInfo });
            }
        }

        private void MusicListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            CommonHelper.GoPage("NowPlayingPage");
        }

        private void LibraryPage_OnCurrentPageChanged()
        {
            this.popup?.HidePopup();
            this.popup2?.HidePopup();
            this.popup3?.HidePopup();
        }
    }
}
