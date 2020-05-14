using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WhatsOnThe.Model;
using WhatsOnThe.Persistance.LocalDb;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;
using WhatsOnTheFridge.Mobile.Core.Dto;

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

    public Task<List<LocationSimpleDto>> GetLocationsNameAsync()
    {
      //todo improve, retrieve only desired columns
      var locationsAsync = GetLocationsAsync();
      locationsAsync.Wait();
      return Task.Run(() =>locationsAsync.Result.Select(i => new LocationSimpleDto {Id = i.Id, Name = i.Name}).ToList());
    }

    public Task<Location> GetLocationAsync(int id)
    {
      return _connection.Table<Location>().Where(i => i.Id == id).FirstOrDefaultAsync();
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
