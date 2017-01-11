using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProjectMato.iOS
{
    public class SecondsToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = (double)value;
            return TimeSpan.FromSeconds(d).ToString(@"mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = (TimeSpan)value;
            return ts.TotalSeconds;
        }

    }
}
