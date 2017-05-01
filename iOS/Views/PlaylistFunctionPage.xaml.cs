using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Common;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.ViewModel;
using UIKit;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistFunctionPage : ContentView
    {
        private bool _isEdit = false;
        public event EventHandler<CommonFunctionEventArgs> OnFinished;

        public PlaylistFunctionPage(PlaylistInfo info)
        {
            InitializeComponent();
            if (info != null)
            {
                _isEdit = true;
                this.BindingContext = new PlaylistFunctionPageViewModel(info);
                this.LabelCreate.Text = "编辑歌单";
            }
            else
            {
                _isEdit = false;
                this.BindingContext = new PlaylistFunctionPageViewModel();
                this.LabelCreate.Text = "添加歌单";
            }
            this.WidthRequest = UIScreen.MainScreen.Bounds.Width;
        }


        private void SubmitButtonButton_OnClicked(object sender, EventArgs e)
        {
            var playlistFunctionPageViewModel = this.BindingContext as PlaylistFunctionPageViewModel;

            if (OnFinished != null && playlistFunctionPageViewModel != null)
                OnFinished(this, new CommonFunctionEventArgs(playlistFunctionPageViewModel.PlaylistInfo, _isEdit ? "Edit" : "Create"));
        }

    }
}
