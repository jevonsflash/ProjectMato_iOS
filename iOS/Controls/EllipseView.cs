using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;



namespace ProjectMato.iOS.Controls
{
	public class EllipseView : View
	{
		public static readonly BindableProperty ColorProperty =
			BindableProperty.Create<EllipseView, Color>(p => p.Color, Color.Accent);

		public Color Color
		{
			get { return (Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}
	}
}
