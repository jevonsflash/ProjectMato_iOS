using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectMato.iOS.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupView : ContentView
    {
        public PopupView()
        {
            InitializeComponent();
        }

        public void ShowPopup(View FunctionPage)
        {
            Popup.Children.Add(FunctionPage);
            Popup.InputTransparent = false;
            this.IsVisible = true;
        }
        public void HidePopup()
        {
            Popup.Children.Clear();
            Popup.InputTransparent = true;
            this.IsVisible = false;
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            HidePopup();
        }
    }
}
