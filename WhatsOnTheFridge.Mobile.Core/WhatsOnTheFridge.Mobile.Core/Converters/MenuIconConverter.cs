using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WhatsOnTheFridge.Mobile.Core.Constants;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.Converters
{
  public class MenuIconConverter: IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var type = (MenuItemType) value;

      switch (type)
      {
        case MenuItemType.Home:
          return "ic_home.png";
        case MenuItemType.Items:
          return "ic_contact.png";
        case MenuItemType.Locations:
          return "ic_pies.png";
        default:
          return string.Empty;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      //Not needed here
      throw new NotImplementedException();
    }
  }
}
