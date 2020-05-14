using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;
using WhatsOnTheFridge.Mobile.Core.Dto;

namespace WhatsOnTheFridge.Mobile.Core.Repositories
{
  public class FakeItemsRepository : IItemsRepository
  {
    List<Item> _items;

    public FakeItemsRepository()
    {

      _items = new List<Item>()
      {
        new Item()
        {
          Id = 1,
          Name = "Lechuga",
          Description = "Verdura",
          AddedDate = DateTime.Now,
          Quantity = 1,
          Location = new Location(){Id = 1, Name = "Freezer"},
          LocationId = 1
        },
        new Item()
        {
          Id = 2,
          Name = "Tomates",
          Description = "Verdura",
          AddedDate = DateTime.Now,
          Quantity = 10,
          Location = new Location(){Id = 1, Name = "Freezer"},
          LocationId = 1
        },
        new Item()
        {
          Id = 3,
          Name = "Merluza",
          Description = "Fish",
          AddedDate = DateTime.Now.AddDays(-20),
          ExpirationDate = DateTime.Now.AddDays(2),
          Quantity = 5,
          Location = new Location(){Id = 2, Name = "Fredge"},
          LocationId = 2
        }
      };
    }

    public Task<List<Item>> GetItemsAsync()
    {
      return Task.Run(() => _items);
    }

    public Task<List<ItemSimpleDto>> GetItemsNameAsync()
    {
      var itemsAsync = GetItemsAsync();
      itemsAsync.Wait();
      var suggestion = new List<ItemSimpleDto>() { new ItemSimpleDto() { Id = 1001, Name = "manzana" }, new ItemSimpleDto() { Id = 1002, Name = "mango" }, new ItemSimpleDto() { Id = 1003, Name = "mandarina" } };
      //return Task.Run(() => (List<ItemSimpleDto>)itemsAsync.Result.Select(i=>new ItemSimpleDto(){Id=i.Id, Name = i.Name}).Concat<ItemSimpleDto>(suggestion));
      return Task.Run(() => itemsAsync.Result.Select(i=>new ItemSimpleDto(){Id=i.Id, Name = i.Name}).ToList().Concat<ItemSimpleDto>(suggestion).ToList());
    }

    public Task<List<Item>> GetItemsNotDoneAsync()
    {
      throw new NotImplementedException();
    }

    public Task<Item> GetItemAsync(int id)
    {
      return Task.Run(() => _items.FirstOrDefault(i => i.Id == id));
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

    public Task<Item> GetItemWithLocationAsync(int id)
    {
      return Task.Run(() => _items.FirstOrDefault(i => i.Id == id));
    }
  }
}
