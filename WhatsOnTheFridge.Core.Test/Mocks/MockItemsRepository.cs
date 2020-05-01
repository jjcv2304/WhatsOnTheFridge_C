using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;

namespace WhatsOnTheFridge.Core.Test.Mocks
{
  public class MockItemsRepository : IItemsRepository
  {
    public List<Item> Items;

    public MockItemsRepository()
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
