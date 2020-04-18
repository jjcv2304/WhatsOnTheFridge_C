using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsOnThe.Model;

namespace WhatsOnTheFridge.Mobile.Core.Repositories
{
  public interface IItemsRepository
  {
    Task<List<Item>> GetItemsAsync();
    Task<List<Item>> GetItemsNotDoneAsync();
    Task<Item> GetItemAsync(int id);
    Task<int> SaveItemAsync(Item item);
    Task<int> DeleteItemAsync(Item item);
  }
}