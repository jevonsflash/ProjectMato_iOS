using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMato.iOS.Helper;
using ProjectMato.iOS.Server;
using ProjectMato.iOS.ViewModel;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public partial class SearchPage : ContentPage
    {
        private NavigationPage detailPage;

        public SearchPage()
        {
            InitializeComponent();
            this.BindingContext = new SearchPageViewModel();
        }

        private void AutoCompleteView_OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            MusicRelatedViewModel.Current.ChangeMusic(e.SelectedItem as MusicInfo);
            CommonHelper.GoPage("NowPlayingPage");
        }
    }
}
