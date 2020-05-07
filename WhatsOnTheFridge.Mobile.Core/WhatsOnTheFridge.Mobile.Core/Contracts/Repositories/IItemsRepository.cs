using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Dto;

namespace WhatsOnTheFridge.Mobile.Core.Contracts.Repositories
{
  public interface IItemsRepository
  {
    Task<List<Item>> GetItemsAsync();
    Task<List<ItemSimpleDto>> GetItemsNameAsync();
    Task<List<Item>> GetItemsNotDoneAsync();
    Task<Item> GetItemAsync(int id);
    Task<int> SaveItemAsync(Item item);
    Task<int> DeleteItemAsync(Item item);
  }
}