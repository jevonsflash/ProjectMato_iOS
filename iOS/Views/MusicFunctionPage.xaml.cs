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
        private IBasicInfo _objInfo;
        public event EventHandler<MusicFunctionEventArgs> OnFinished;
        public MusicFunctionPage(IBasicInfo objInfo, IList<MenuCellInfo> menus)
        {
            InitializeComponent();
            this._objInfo = objInfo;
            this.WidthRequest = UIScreen.MainScreen.Bounds.Width;
            this.BindingContext = new MusicFunctionPageViewModel(objInfo, menus);



        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

                OnFinished?.Invoke(this,new MusicFunctionEventArgs(_objInfo, e.SelectedItem as MenuCellInfo));
        }
    }
    public class MusicFunctionEventArgs : EventArgs
    {
        public MusicFunctionEventArgs(IBasicInfo musicInfo, MenuCellInfo menuCellInfo)
        {
            this.MusicInfo = musicInfo;
            this.MenuCellInfo = menuCellInfo;
        }
        public IBasicInfo MusicInfo { get; set; }
        public MenuCellInfo MenuCellInfo { get; set; }
    }



}
