using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsOnThe.Model;

namespace WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data
{
  public interface ILocationsService
  {
    Task AddLocation(Location newLocation);
    Task<Location> GetLocationAsync(int id);
    Task<IEnumerable<Location>> GetAllLocationsAsync();
  }
}