using ProjectMato.iOS.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class AboutPage : ContentPage
    {


        AboutPageViewModel viewModel;

        public AboutPage()
        {
            InitializeComponent();
            viewModel = new AboutPageViewModel();
            BindingContext = viewModel;
        }


        private void BTNGoAPP1_Click(object sender, EventArgs e)
        {
            
        }

        private void BTNGoAPP2_Click(object sender, EventArgs e)
        {
            
        }

        private void BTNGoAPP3_Click(object sender, EventArgs e)
        {
             
        }
    }

}
