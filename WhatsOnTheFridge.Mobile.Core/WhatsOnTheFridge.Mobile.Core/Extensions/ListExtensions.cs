using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WhatsOnTheFridge.Mobile.Core.Dto;

namespace WhatsOnTheFridge.Mobile.Core.Extensions
{
  public static class ListExtensions
  {
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
    {
      var collection = new ObservableCollection<T>();
      if (list == null) return collection;
      foreach (var item in list)
      {
        collection.Add(item);
      }

      return collection;
    }
  }
}
