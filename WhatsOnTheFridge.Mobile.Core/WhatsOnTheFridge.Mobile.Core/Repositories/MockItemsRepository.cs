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
        },        
        new Item()
        {
          Id = 2,
          Name = "Tomates",
          Description = "Verdura",
          AddedDate = DateTime.Now,
        },
        new Item()
        {
          Id = 3,
          Name = "Merluza",
          Description = "Fish",
          AddedDate = DateTime.Now.AddDays(-20),
          ExpirationDate = DateTime.Now.AddDays(2)
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
      throw new NotImplementedException();
    }

    public Task<int> DeleteItemAsync(Item item)
    {
      throw new NotImplementedException();
    }
  }
}
