using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Dto;

namespace WhatsOnTheFridge.Mobile.Core.Contracts.Repositories
{
  public interface ILocationsRepository
  {
    Task<List<Location>> GetLocationsAsync();
    Task<List<LocationSimpleDto>> GetLocationsNameAsync();
    Task<Location> GetLocationAsync(int id);
    Task<int> SaveLocationAsync(Location location);
  }
}