using ProjectMato.iOS.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using ProjectMato.iOS.Renders;


[assembly: ExportRenderer(typeof(EllipseView), typeof(EllipseRenderer))]
namespace ProjectMato.iOS.Renders
{
	public class EllipseRenderer : VisualElementRenderer<EllipseView>
	{
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == EllipseView.ColorProperty.PropertyName)
				this.SetNeedsDisplay(); // Force a call to Draw
		}

		public override void Draw(CGRect rect)
		{
			using (var context = UIGraphics.GetCurrentContext())
			{
				var path = CGPath.EllipseFromRect(rect);
				context.AddPath(path);
				context.SetFillColor(this.Element.Color.ToCGColor());
				context.DrawPath(CGPathDrawingMode.Fill);
			}
		}
	}
}