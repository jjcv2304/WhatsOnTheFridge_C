using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Dto;

namespace WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data
{
  public interface IItemsService
  {
    Task<Item> GetItemAsync(int id);
    Task<Item> GetItemWithLocationAsync(int id);
    Task<IEnumerable<Item>> GetAllItemsAsync();
    Task<List<ItemSimpleDto>> GetAllItemsNameAsync();
    Task AddItem(Item newItem);
    Task RemoveItem(Item deleteItem);
    Task ModifyItem(Item updateItem);
  }
}