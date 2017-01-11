using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ProjectMato.iOS.Controls.GeneralListView), typeof(ProjectMato.iOS.Renders.GeneralListViewRenderer))]

namespace ProjectMato.iOS.Renders
{

    public class GeneralListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            var listView = Control;

            listView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			listView.BackgroundColor = Color.Transparent.ToUIColor();
        }
    }
}
