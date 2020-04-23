using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;

namespace WhatsOnTheFridge.Mobile.Core.Repositories
{
  public class MockItemsRepository : IItemsRepository
  {
    List<Item> _items;

    public MockItemsRepository()
    {
      _items = new List<Item>()
      {
        new Item()
        {
          Id = 1,
          Name = "Lechuga",
          Description = "Verdura",
          AddedDate = DateTime.Now,
          Quantity = 1
        },        
        new Item()
        {
          Id = 2,
          Name = "Tomates",
          Description = "Verdura",
          AddedDate = DateTime.Now,
          Quantity = 10
        },
        new Item()
        {
          Id = 3,
          Name = "Merluza",
          Description = "Fish",
          AddedDate = DateTime.Now.AddDays(-20),
          ExpirationDate = DateTime.Now.AddDays(2),
          Quantity = 5
        }
      };
    }

    public Task<List<Item>> GetItemsAsync()
    {
      return Task.Run(() => _items);
    }

    public Task<List<Item>> GetItemsNotDoneAsync()
    {
      throw new NotImplementedException();
    }

    public Task<Item> GetItemAsync(int id)
    {
      return Task.Run(() => _items.FirstOrDefault(i=>i.Id==id));
    }

    public Task<int> SaveItemAsync(Item item)
    {
      if (item.Id != 0)
      {
        var index = _items.FindIndex(i => i.Id == item.Id);
        _items[index] = item;
        return Task.FromResult(item.Id);
      }
      else
      {
        var lastId = _items.Max(i => i.Id);
        item.Id = lastId + 1;
        _items.Add(item);
        return Task.FromResult(item.Id);
      }
    }

    public Task<int> DeleteItemAsync(Item item)
    {
      throw new NotImplementedException();
    }
  }
}
