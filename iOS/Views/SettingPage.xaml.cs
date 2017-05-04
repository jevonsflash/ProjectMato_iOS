using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            CommonHelper.GoNavigate("SleepModePage");
        }
    }
}
