using System;
using System.Collections.Generic;
using System.Text;
using WhatsOnThe.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WhatsOnTheFridge.Core.Test.ViewModelsApprovalTests.Extensions
{
  public static class ListItemsViewModelsApprovalTestExtensions
  {
    public static string ToApprovalString(this List<Item> items)
    {
      var sb = new StringBuilder();

      sb.AppendFormat("Total items on list {0}", items.Count);
      sb.AppendLine();

      items.ForEach(i =>
      {
        var jsonItem = JsonSerializer.Serialize(i);
        sb.Append(jsonItem);
        sb.AppendLine();
      });

      return sb.ToString();
    }
  }

}
