using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Dto;

namespace WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data
{
  public interface ILocationsService
  {
    Task AddLocation(Location newLocation);
    Task<Location> GetLocationAsync(int id);
    Task<List<LocationSimpleDto>> GetAllLocationsNameAsync();
    Task<IEnumerable<Location>> GetAllLocationsAsync();
  }
}