using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.ViewModel;
using UIKit;
using Xamarin.Forms;
using XLabs.Platform.Device;


namespace ProjectMato.iOS
{
    public partial class MusicFunctionPage
    {
        private MusicInfo _musicInfo;
        public event EventHandler<MusicFunctionEventArgs> OnFinished;
        public MusicFunctionPage(MusicInfo musicInfo, IList<MenuCellInfo> menus)
        {
            InitializeComponent();
            this._musicInfo = musicInfo;
            this.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.BindingContext = new MusicFunctionPageViewModel(musicInfo, menus);



        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

                OnFinished?.Invoke(this,new MusicFunctionEventArgs(_musicInfo, e.SelectedItem as MenuCellInfo));
        }
    }
    public class MusicFunctionEventArgs : EventArgs
    {
        public MusicFunctionEventArgs(MusicInfo musicInfo, MenuCellInfo menuCellInfo)
        {
            this.MusicInfo = musicInfo;
            this.MenuCellInfo = menuCellInfo;
        }
        public MusicInfo MusicInfo { get; set; }
        public MenuCellInfo MenuCellInfo { get; set; }
    }



}
