using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            this.BindingContext = new SettingPageViewModel();
           // this.BackgroundFliperView.ItemTemplate = new DataTemplate(typeof(BackgroundFliperView));

        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SleepModePage());
        }
    }
}
