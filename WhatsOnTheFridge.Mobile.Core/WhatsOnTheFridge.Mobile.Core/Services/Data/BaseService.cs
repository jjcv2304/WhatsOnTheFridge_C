using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using WhatsOnThe.Model;

namespace WhatsOnTheFridge.Mobile.Core.Services.Data
{
  public class BaseService
  {
    protected IBlobCache Cache;

    public BaseService(IBlobCache cache)
    {
      Cache = cache ?? BlobCache.LocalMachine;
    }

    internal async Task<T> GetFromCache<T>(string cacheName)
    {
      try
      {
        T t = await Cache.GetObject<T>(cacheName);
        return t;
      }
      catch (KeyNotFoundException)
      {
        return default(T);
      }
    }

    internal void InvalidateCache()
    {
      Cache.InvalidateAllObjects<Item>();
      Cache.InvalidateAllObjects<Location>();
    }
  }
}
