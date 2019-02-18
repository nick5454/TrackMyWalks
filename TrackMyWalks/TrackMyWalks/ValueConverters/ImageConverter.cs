using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;

namespace TrackMyWalks.ValueConverters
{
    public class ImageConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var DiffLevel = (String)value;

			switch(DiffLevel)
			{
				case "Easy":
					return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g1.jpeg";
				case "Moderate":
					return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g2.jpeg";
				case "Challenging":
				case "Difficult":
					return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g3.jpeg";
				case "Very Difficult":
				case "Extreme":
					return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g5.jpeg";
				default:
					return "http://www.trailhiking.com.au/wp-content/uploads/2013/08/g1.jpeg";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
    }
}
