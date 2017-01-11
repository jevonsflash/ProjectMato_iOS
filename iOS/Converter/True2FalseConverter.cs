using System;
using Xamarin.Forms;
using System.Globalization;

namespace ProjectMato.iOS
{
	public class True2FalseConverter:IValueConverter
	{
		public True2FalseConverter ()
		{
			
		}

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !((bool)value);
		}
		public object ConvertBack(object value, Type targetType, object parameter,CultureInfo culture)
		{
			return !((bool)value);
		}
	}
}

