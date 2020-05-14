using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;
using WhatsOnTheFridge.Mobile.Core.Dto;

namespace WhatsOnTheFridge.Core.Test.Fakes
{
  public class FakeLocationsRepository: ILocationsRepository
  {
    public List<Location> _locations { get; set; }

    public FakeLocationsRepository()
    {
      _locations = new List<Location>()
      {
        new Location()
        {
          Id = 1,
          Name = "Freezer",
          Description = "-10 C°",
        },
        new Location()
        {
          Id = 2,
          Name = "Fridge",
          Description = "4-10 C°",
        },
        new Location()
        {
          Id = 3,
          Name = "Black Shelf",
          Description = "20C°",
        }
      };
    }

    public Task<List<Location>> GetLocationsAsync()
    {
      return Task.Run(() => _locations);
    }
    
    public Task<Location> GetLocationAsync(int id)
    {
      return Task.Run(() => _locations.FirstOrDefault(i => i.Id == id));
    }
    public Task<List<LocationSimpleDto>> GetLocationsNameAsync()
    {
      var locationsAsync = GetLocationsAsync();
      locationsAsync.Wait();
      return Task.Run(() =>locationsAsync.Result.Select(i => new LocationSimpleDto {Id = i.Id, Name = i.Name}).ToList());
    }

    public Task<int> SaveLocationAsync(Location location)
    {
      if (location.Id != 0)
      {
        var index = _locations.FindIndex(i => i.Id == location.Id);
        _locations[index] = location;
        return Task.FromResult(location.Id);
      }
      else
      {
        var lastId = _locations.Max(i => i.Id);
        location.Id = lastId + 1;
        _locations.Add(location);
        return Task.FromResult(location.Id);
      }
    }
  }
}
