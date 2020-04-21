using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Constants;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Repositories;

namespace WhatsOnTheFridge.Mobile.Core.Services.Data
{
  public class ItemsService : BaseService, IItemsService
  {
    private readonly IItemsRepository _itemsRepository;

    public ItemsService(IItemsRepository itemsRepository, IBlobCache cache = null) : base(cache)
    {
      _itemsRepository = itemsRepository;
    }

    public async Task<IEnumerable<Item>> GetAllItemsAsync()
    {
      var itemsFromCache = await GetFromCache<List<Item>>(CacheNameConstants.AllItems);

      if (itemsFromCache != null)
      {
        return itemsFromCache;
      }
      else
      {
        var items = await _itemsRepository.GetItemsAsync();
        await Cache.InsertObject(CacheNameConstants.AllItems, items, DateTimeOffset.Now.AddMinutes(1));
        return items;
      }
    }

    public async Task AddItem(Item newItem)
    {
      await _itemsRepository.SaveItemAsync(newItem);

      await Cache.InvalidateAllObjects<Item>();
    }
    
    public async Task RemoveItem(Item deleteItem)
    {
      await _itemsRepository.DeleteItemAsync(deleteItem);

      await Cache.InvalidateAllObjects<Item>();
    }
    
    public async Task ModifyItem(Item updateItem)
    {
      await _itemsRepository.SaveItemAsync(updateItem);

      await Cache.InvalidateAllObjects<Item>();
    }

  }
}
