using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.ViewModel;
using UIKit;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistFunctionPage : ContentView
    {
        public event EventHandler<PlaylistFunctionEventArgs> OnFinished;

        public PlaylistFunctionPage(PlaylistFunctionMenuType type, PlaylistInfo info)
        {
            InitializeComponent();
            if (info != null)
            {
                this.BindingContext = new PlaylistFunctionPageViewModel(info);
            }
            else
            {
                this.BindingContext = new PlaylistFunctionPageViewModel();
            }
            this.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.CancelButton.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            if (type == PlaylistFunctionMenuType.Create)
            {
                this.LabelCreate.Text = "添加歌单";
            }

            else if (type == PlaylistFunctionMenuType.Edit)
            {
                this.LabelCreate.Text = "编辑歌单";

            }

        }


        private void SubmitButtonButton_OnClicked(object sender, EventArgs e)
        {
            var playlistFunctionPageViewModel = this.BindingContext as PlaylistFunctionPageViewModel;

            if (OnFinished != null && playlistFunctionPageViewModel != null)
                OnFinished(this, new PlaylistFunctionEventArgs(PlaylistFunctionType.Submit, playlistFunctionPageViewModel.PlaylistInfo));
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            var playlistFunctionPageViewModel = this.BindingContext as PlaylistFunctionPageViewModel;

            if (OnFinished != null && playlistFunctionPageViewModel != null)
                OnFinished(this, new PlaylistFunctionEventArgs(PlaylistFunctionType.Cancel, playlistFunctionPageViewModel.PlaylistInfo));
        }
    }
    public class PlaylistFunctionEventArgs : EventArgs
    {
        public PlaylistFunctionEventArgs(PlaylistFunctionType functionType, PlaylistInfo playlistInfo)
        {

            this.FunctionType = functionType;
            this.PlaylistInfo = playlistInfo;
        }

        public PlaylistFunctionType FunctionType { get; set; }

        public PlaylistInfo PlaylistInfo { get; set; }
    }

    public enum PlaylistFunctionType
    {
        Submit, Cancel
    }

    public enum PlaylistFunctionMenuType
    {
        Create, Edit
    }

}
