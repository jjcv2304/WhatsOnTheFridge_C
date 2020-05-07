using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Core.Test.Builders;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;
using WhatsOnTheFridge.Mobile.Core.Dto;

namespace WhatsOnTheFridge.Core.Test.Fakes
{
  public class FakeItemsRepository : IItemsRepository
  {
    public List<Item> Items;

    public FakeItemsRepository()
    {
      Items = new List<Item>();
      for (int i = 0; i < 30; i++)
      {
        Items.Add(ItemBuilder.TypicalWId().Build());
      }
    }

    public Task<List<Item>> GetItemsAsync()
    {
      return Task.Run(() => Items);
    }

    public Task<List<ItemSimpleDto>> GetItemsNameAsync()
    {
      var itemsAsync = GetItemsAsync();
      itemsAsync.Wait();
      return Task.Run(() => itemsAsync.Result.Select(i=>new ItemSimpleDto(){Id=i.Id, Name = i.Name}).ToList());
    }

    public Task<List<Item>> GetItemsNotDoneAsync()
    {
      throw new NotImplementedException();
    }

    public Task<Item> GetItemAsync(int id)
    {
      return Task.Run(() => Items.FirstOrDefault(i=>i.Id==id));
    }

    public Task<int> SaveItemAsync(Item item)
    {
      if (item.Id != 0)
      {
        var index = Items.FindIndex(i => i.Id == item.Id);
        Items[index] = item;
        return Task.FromResult(item.Id);
      }
      else
      {
        var lastId = Items.Max(i => i.Id);
        item.Id = lastId + 1;
        Items.Add(item);
        return Task.FromResult(item.Id);
      }
    }

    public Task<int> DeleteItemAsync(Item item)
    {
      throw new NotImplementedException();
    }
  }
}
