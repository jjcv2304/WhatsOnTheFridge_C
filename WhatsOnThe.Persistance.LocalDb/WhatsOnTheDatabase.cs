using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using WhatsOnThe.Model;

namespace WhatsOnThe.Persistance.LocalDb
{
  public class WhatsOnTheDatabase
  {
    static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
    {
      return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
    });

    static SQLiteAsyncConnection Database => lazyInitializer.Value;
    static bool initialized = false;

    public WhatsOnTheDatabase()
    {
      InitializeAsync().SafeFireAndForget(false);
    }

    async Task InitializeAsync()
    {
      if (!initialized)
      {
        if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Location).Name))
        {
          await Database.CreateTablesAsync(CreateFlags.None, typeof(Location)).ConfigureAwait(false);
          initialized = true;
        }
        if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Item).Name))
        {
          await Database.CreateTablesAsync(CreateFlags.AllImplicit, typeof(Item)).ConfigureAwait(false);
          initialized = true;
        }
        
        initialized = true;
      }
    }

    public Task<List<Item>> GetItemsAsync()
    {
      return Database.Table<Item>().ToListAsync();
    }
    public Task<List<Location>> GetLocationsAsync()
    {
      return Database.Table<Location>().ToListAsync();
    }

    public Task<List<Item>> GetItemsNotDoneAsync()
    {
      // SQL queries are also possible
      return Database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [Done] = 0");
    }

    public Task<Item> GetItemAsync(int id)
    {
      return Database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public Task<int> SaveItemAsync(Item item)
    {
      if (item.Id != 0)
      {
        return Database.UpdateAsync(item);
      }
      else
      {
        return Database.InsertAsync(item);
      }
    }
    public Task<int> SaveLocationAsync(Location location)
    {
      if (location.Id != 0)
      {
        return Database.UpdateAsync(location);
      }
      else
      {
        return Database.InsertAsync(location);
      }
    }

    public Task<int> DeleteItemAsync(Item item)
    {
      return Database.DeleteAsync(item);
    }

  }
}
