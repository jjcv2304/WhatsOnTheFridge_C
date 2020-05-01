using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsOnTheFridge.Core.Test.Mocks
{
  public partial class ItemBuilder
  {
    public static ItemBuilder Simple()
    {
      return Default()
        .WithName(() => GetRandom.String(1, 50));
    }
    public static ItemBuilder Typical()
    {
      return Simple()
        .WithDescription(() => GetRandom.String(1, 150))
        .WithAddedDate(GetRandom.DateTime())
        .WithQuantity(GetRandom.Int16(10));
    }
    public static ItemBuilder TypicalWId()
    {
      return Typical()
        .WithId(GetRandom.Id);
    }
    //protected override void PostBuild(Category value)
    //{
    //  foreach (var childCategory in value?.ChildCategories ?? Enumerable.Empty<Category>())
    //  {
    //    childCategory.ParentCategory = value;
    //    //childCategory.ParentCategoryId = value?.Id;
    //  }
    //}
  }

}
