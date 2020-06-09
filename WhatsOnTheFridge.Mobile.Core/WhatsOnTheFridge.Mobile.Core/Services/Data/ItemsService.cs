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
using WhatsOnTheFridge.Mobile.Core.Dto;
using WhatsOnTheFridge.Mobile.Core.Repositories;

namespace WhatsOnTheFridge.Mobile.Core.Services.Data
{
  public class ItemsService : BaseService, IItemsService
  {
    private readonly IItemsRepository _itemsRepository;

    public ItemsService(IItemsRepository itemsRepository) : base(null)
    {
      _itemsRepository = itemsRepository;
    }
    
    public ItemsService(IItemsRepository itemsRepository, IBlobCache cache) : base(cache)
    {
      _itemsRepository = itemsRepository;
    }

    public Task<Item> GetItemAsync(int id)
    {
      return _itemsRepository.GetItemAsync(id);
    }

    public Task<Item> GetItemWithLocationAsync(int id)
    {
      return _itemsRepository.GetItemWithLocationAsync(id);
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
   
    public async Task<List<ItemSimpleDto>> GetAllItemsNameAsync()
    {
      var itemsFromCache = await GetFromCache<List<ItemSimpleDto>>(CacheNameConstants.AllItemsName);

      if (itemsFromCache != null)
      {
        return itemsFromCache;
      }
      else
      {
        var items = await _itemsRepository.GetItemsNameAsync();
        await Cache.InsertObject(CacheNameConstants.AllItemsName, items, DateTimeOffset.Now.AddMinutes(1));
        return items;
      }
    }

    public async Task AddItem(Item newItem)
    {
      await _itemsRepository.SaveItemAsync(newItem);
      await Cache.Invalidate(CacheNameConstants.AllItems);
      await Cache.Invalidate(CacheNameConstants.AllItemsName);
      //await Cache.InvalidateAllObjects<Item>();
      //await Cache.InvalidateAll();
      //await Cache.InvalidateAllObjects();
    }

    public async Task RemoveItem(Item deleteItem)
    {
      await _itemsRepository.DeleteItemAsync(deleteItem);

      await Cache.InvalidateAllObjects<Item>();
    }

    public async Task ModifyItem(Item updateItem)
    {
      await _itemsRepository.SaveItemAsync(updateItem);

      var itemsInCache = await GetFromCache<List<Item>>(CacheNameConstants.AllItems);
      if (itemsInCache == null)
        return;

      var index = itemsInCache.FindIndex(i => i.Id == updateItem.Id);
      itemsInCache[index] = updateItem;
      await Cache.InsertObject(CacheNameConstants.AllItems, itemsInCache, DateTimeOffset.Now.AddMinutes(1));
    }

  }
}
