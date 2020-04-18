using System;
using System.Collections.Generic;
using System.Text;
using TinyIoC;
using WhatsOnThe.Persistance.LocalDb;
using WhatsOnTheFridge.Mobile.Core.Repositories;

namespace WhatsOnTheFridge.Mobile.Core.Bootstrap
{
  
  public static class TinyIoCSetup
  {
    private static TinyIoCContainer _container;

    static TinyIoCSetup()
    {
      _container = new TinyIoCContainer();

           
      // View models - by default, TinyIoC will register concrete classes as multi-instance.
      ///_container.Register<BasketViewModel>();

      // Services - by default, TinyIoC will register interface registrations as singletons.
      //_container.Register<INavigationService, NavigationService>();
    }
  }
}
