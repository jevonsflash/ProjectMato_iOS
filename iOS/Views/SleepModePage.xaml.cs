using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class SleepModePage : ContentPage
    {
        public SleepModePage()
        {

            InitializeComponent();
            this.BindingContext=new SleepModePageViewModel();
        }
    }
}
