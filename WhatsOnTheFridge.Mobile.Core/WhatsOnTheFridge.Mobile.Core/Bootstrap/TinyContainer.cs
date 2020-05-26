using System;
using System.Collections.Generic;
using System.Text;
using TinyIoC;
using WhatsOnThe.Persistance.LocalDb;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Repositories;
using WhatsOnTheFridge.Mobile.Core.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels;

namespace WhatsOnTheFridge.Mobile.Core.Bootstrap
{
  
  public static class TinyContainer
  {
    private static TinyIoCContainer _container;

    public static void RegisterDependencies()
    {
      _container = new TinyIoCContainer();

      // View models - by default, TinyIoC will register concrete classes as multi-instance.
      _container.Register<MainViewModel>();
      _container.Register<MenuViewModel>();
      _container.Register<HomeViewModel>();
      _container.Register<MainMenuItemViewModel>();
      _container.Register<ItemsListViewModel>();
      _container.Register<ItemDetailViewModel>();
      _container.Register<ItemNewViewModel>();
      _container.Register<LocationNewViewModel>();
      _container.Register<ItemsListGroupByLocationViewModel>();

      //Services data
      _container.Register<IItemsService, ItemsService>();
      _container.Register<ILocationsService, LocationsService>();

      // Services - by default, TinyIoC will register interface registrations as singletons.
      _container.Register<INavigationService, NavigationService>();
      _container.Register<IDialogService, DialogService>();

      //Repositories
      //_container.Register<IItemsRepository, ItemsRepository>();
      _container.Register<IItemsRepository, FakeItemsRepository>();
      _container.Register<ILocationsRepository, FakeLocationsRepository>();
    }

    public static object Resolve(Type typeName)
    {
      return _container.Resolve(typeName);
    }

    public static T Resolve<T>() where T : class
    {
      return _container.Resolve<T>();
    }
  }
}
