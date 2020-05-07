using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsOnThe.Model;

namespace WhatsOnTheFridge.Mobile.Core.Contracts.Repositories
{
  public interface ILocationsRepository
  {
    Task<List<Location>> GetLocationsAsync();
    Task<Location> GetLocationAsync(int id);
    Task<int> SaveLocationAsync(Location location);
  }
}