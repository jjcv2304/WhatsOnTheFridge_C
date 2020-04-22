using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.Converters
{
  public class ShortDateConverter: IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value == null)
        return "N/A";

      var datetime = (DateTime)value;
      return datetime.ToLocalTime().ToShortDateString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException(); 
    }
  }
}
