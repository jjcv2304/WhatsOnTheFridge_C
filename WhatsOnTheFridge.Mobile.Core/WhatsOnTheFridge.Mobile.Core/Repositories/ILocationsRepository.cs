using System.Collections.Generic;
using System.Threading.Tasks;
using WhatsOnThe.Model;

namespace WhatsOnTheFridge.Mobile.Core.Repositories
{
  public interface ILocationsRepository
  {
    Task<List<Location>> GetLocationsAsync();
    Task<int> SaveLocationAsync(Location location);
  }
}