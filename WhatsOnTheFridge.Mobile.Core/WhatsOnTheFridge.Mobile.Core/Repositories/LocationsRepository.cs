using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WhatsOnThe.Model;
using WhatsOnThe.Persistance.LocalDb;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;

namespace WhatsOnTheFridge.Mobile.Core.Repositories
{
  public class LocationsRepository : ILocationsRepository
  {
    private readonly SQLiteAsyncConnection _connection;

    public LocationsRepository(WhatsOnTheDatabase database)
    {
      _connection = database.Connection;
    }

    public Task<List<Location>> GetLocationsAsync()
    {
      return _connection.Table<Location>().ToListAsync();
    }

    public Task<int> SaveLocationAsync(Location location)
    {
      if (location.Id != 0)
      {
        return _connection.UpdateAsync(location);
      }
      else
      {
        return _connection.InsertAsync(location);
      }
    }

  }
} 
