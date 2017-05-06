using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using ProjectMato.iOS.Controls;
using ProjectMato.iOS.Enums;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ProjectMato.iOS
{

    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            this.BindingContext = MenuPageViewModel.Current;
        }
    }

}
