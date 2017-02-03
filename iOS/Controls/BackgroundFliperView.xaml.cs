using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Model;
using ProjectMato.iOS.Server;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS
{
    public partial class BackgroundFliperView
    {
        public BackgroundFliperView()
        {
            InitializeComponent();
        }

        private void IsSelButton_OnClicked(object sender, EventArgs e)
        {
            var settingPageViewModel = this.Parent.BindingContext as SettingPageViewModel;
            var backgroundTable = this.BindingContext as BackgroundTable;
            if (backgroundTable != null && settingPageViewModel != null)
            {
                backgroundTable.IsSel = !backgroundTable.IsSel;
                settingPageViewModel.BackgroundList[settingPageViewModel.BackgroundList.IndexOf(backgroundTable)] =
                    backgroundTable;

            }
        }
    }

}
