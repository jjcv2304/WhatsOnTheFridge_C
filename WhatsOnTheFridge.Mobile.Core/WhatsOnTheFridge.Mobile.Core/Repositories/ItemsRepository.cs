using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WhatsOnThe.Model;
using WhatsOnThe.Persistance.LocalDb;

namespace WhatsOnTheFridge.Mobile.Core.Repositories
{
  public class ItemsRepository : IItemsRepository
  {
    private readonly SQLiteAsyncConnection _connection;

    public ItemsRepository(WhatsOnTheDatabase database)
    {
      _connection = database.Connection;
    }

    public Task<List<Item>> GetItemsAsync()
    {
      return _connection.Table<Item>().ToListAsync();
    }

    public Task<List<Item>> GetItemsNotDoneAsync()
    {
      // SQL queries are also possible
      return _connection.QueryAsync<Item>("SELECT * FROM [Item] WHERE [Done] = 0");
    }

    public Task<Item> GetItemAsync(int id)
    {
      return _connection.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public Task<int> SaveItemAsync(Item item)
    {
      if (item.Id != 0)
      {
        return _connection.UpdateAsync(item);
      }
      else
      {
        return _connection.InsertAsync(item);
      }
    }

    public Task<int> DeleteItemAsync(Item item)
    {
      return _connection.DeleteAsync(item);
    }

  }
}
