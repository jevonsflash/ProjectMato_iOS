using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly:ExportRenderer(typeof(ProjectMato.iOS.MenuTableView), typeof(ProjectMato.iOS.Renders.MenuTableViewRenderer))]
namespace ProjectMato.iOS.Renders
{
	public class MenuTableViewRenderer : TableViewRenderer 
	{

		protected override void OnElementChanged (ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged (e);
		
			var tableView = Control as UITableView;

			tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			tableView.BackgroundColor = App.MenuBackgroundColor.ToUIColor();
		}
	}
}

