using WhatsOnTheFridge.Core.Test.Fakes;

namespace WhatsOnTheFridge.Core.Test.Builders
{
  public partial class LocationBuilder
  {
    public static LocationBuilder Simple()
    {
      return Default()
        .WithName(() => GetRandom.String(1, 50));
    }
    public static LocationBuilder Typical()
    {
      return Simple()
        .WithDescription(() => GetRandom.String(1, 150));
    }
    public static LocationBuilder TypicalWId()
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
