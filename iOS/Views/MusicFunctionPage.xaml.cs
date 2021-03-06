﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.ViewModel;
using UIKit;
using Xamarin.Forms;
using XLabs.Platform.Device;
using ProjectMato.iOS.Server;

namespace ProjectMato.iOS
{
    public partial class MusicFunctionPage
    {
        private IBasicInfo _objInfo;
        public event EventHandler<MusicFunctionEventArgs> OnFinished;
        public MusicFunctionPage(IBasicInfo objInfo, IList<MenuCellInfo> menus)
        {
            InitializeComponent();
            this._objInfo = objInfo;
            this.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.BindingContext = new MusicFunctionPageViewModel(objInfo, menus);
            if (_objInfo is MusicInfo)
            {
                this.FavouritLayout.IsVisible = true;
            }


        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            OnFinished?.Invoke(this, new MusicFunctionEventArgs(_objInfo, e.SelectedItem as MenuCellInfo));
        }

        private void IsFavouriteButton_OnClicked(object sender, EventArgs e)
        {


            var musicInfo = _objInfo as MusicInfo;
            if (musicInfo != null)
            {
                if (!musicInfo.IsFavourite)
                {

                    if (MusicInfoServer.Current.CreatePlaylistEntryToMyFavourite(musicInfo))
                    {
                        musicInfo.IsFavourite = true;
                    }

                }
                else
                {
                    if (MusicInfoServer.Current.DeletePlaylistEntryFromMyFavourite(musicInfo))
                    {
                        musicInfo.IsFavourite = false;
                    }

                }
            }
        }

    }
}
