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

namespace WhatsOnTheFridge.Mobile.Core.Services.Data
{
  public class LocationsService : BaseService, ILocationsService
  {
    private readonly ILocationsRepository _locationsRepository;

    public LocationsService(ILocationsRepository locationsRepository) : base(null)
    {
      _locationsRepository = locationsRepository;
    }
    public LocationsService(ILocationsRepository locationsRepository, IBlobCache cache) : base(cache)
    {
      _locationsRepository = locationsRepository;
    }

    public Task<Location> GetLocationAsync(int id)
    {
      return _locationsRepository.GetLocationAsync(id);
    }

    public async Task<IEnumerable<Location>> GetAllLocationsAsync()
    {
      var locationsFromCache = await GetFromCache<List<Location>>(CacheNameConstants.AllLocations);

      if (locationsFromCache != null)
      {
        return locationsFromCache;
      }
      else
      {
        var locations = await _locationsRepository.GetLocationsAsync();
        await Cache.InsertObject(CacheNameConstants.AllLocations, locations, DateTimeOffset.Now.AddMinutes(1));
        return locations;
      }
    }

    public async Task AddLocation(Location newLocation)
    {
      await _locationsRepository.SaveLocationAsync(newLocation);
      await Cache.InvalidateAllObjects<Location>();
    }

    public async Task<List<LocationSimpleDto>> GetAllLocationsNameAsync()
    {
      var locationsFromCache = await GetFromCache<List<LocationSimpleDto>>(CacheNameConstants.AllLocationsName);

      if (locationsFromCache != null)
      {
        return locationsFromCache;
      }
      else
      {
        var locations = await _locationsRepository.GetLocationsNameAsync();
        await Cache.InsertObject(CacheNameConstants.AllLocationsName, locations, DateTimeOffset.Now.AddMinutes(1));
        return locations;
      }
    }
  }
}
