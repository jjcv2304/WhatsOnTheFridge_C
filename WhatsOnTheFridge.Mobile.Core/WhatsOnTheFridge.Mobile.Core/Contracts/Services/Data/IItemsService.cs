using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsOnThe.Model;

namespace WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data
{
  public interface IItemsService
  {
    Task<IEnumerable<Item>> GetAllItemsAsync();
    Task AddItem(Item newItem);
    Task RemoveItem(Item deleteItem);
    Task ModifyItem(Item updateItem);
  }
}