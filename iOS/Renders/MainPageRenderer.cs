using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XLabs.Forms.Controls;

[assembly: ExportRenderer(typeof(ProjectMato.iOS.AboutPage), typeof(ProjectMato.iOS.Renders.MainPageRenderer))]
[assembly: ExportRenderer(typeof(ProjectMato.iOS.LibraryPage), typeof(ProjectMato.iOS.Renders.LibraryPageRenderer))]
[assembly: ExportRenderer(typeof(ProjectMato.iOS.Controls.LibraryGridView), typeof(ProjectMato.iOS.Renders.LibraryGridViewRenderer))]

namespace ProjectMato.iOS.Renders
{
    public class MainPageRenderer : PageRenderer
    {
        public MainPageRenderer()
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationItem.SetHidesBackButton(true, true);
        }
    }

    public class LibraryPageRenderer : ExtendedTabbedPageRenderer
    {
        public LibraryPageRenderer()
        {
            TabBar.TintColor = UIColor.Yellow;
            TabBar.BarTintColor =UIColor.Red;
            TabBar.BackgroundColor = UIColor.Green;
            
        }

    }
    public class LibraryGridViewRenderer:GridViewRenderer
    {
        public LibraryGridViewRenderer()
        {
            
        }
    }
}

