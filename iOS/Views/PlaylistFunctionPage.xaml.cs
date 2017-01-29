using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.ViewModel;
using UIKit;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class PlaylistFunctionPage : ContentView
    {
        public event EventHandler<PlaylistFunctionEventArgs> OnFinished;

        public PlaylistFunctionPage(PlaylistFunctionMenuType type)
        {
            InitializeComponent();
            var createPlaylistPageViewModel = new PlaylistFunctionPageViewModel();
            this.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.CancelButton.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.BindingContext = createPlaylistPageViewModel;
            if (type == PlaylistFunctionMenuType.Create)
            {

            }

            else if (type == PlaylistFunctionMenuType.Edit)
            {

            }
        }

        private void SubmitButtonButton_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new PlaylistFunctionEventArgs(PlaylistFunctionType.Submit));
        }



        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, new PlaylistFunctionEventArgs(PlaylistFunctionType.Cancel));
        }
    }
    public class PlaylistFunctionEventArgs : EventArgs
    {
        public PlaylistFunctionEventArgs(PlaylistFunctionType functionType)
        {

            this.FunctionType = functionType;
        }

        public PlaylistFunctionType FunctionType { get; set; }
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
