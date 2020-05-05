using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using WhatsOnTheFridge.Mobile.Core.Views;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.Services.General
{
  public class NavigationService : INavigationService
  {
    private readonly Dictionary<Type, Type> _mappings;

    protected Application CurrentApplication => Application.Current;

    public NavigationService()
    {
      _mappings = new Dictionary<Type, Type>();

      CreatePageViewModelMappings();
    }

    public async Task InitializeAsync()
    {
      await NavigateToAsync<MainViewModel>();
    }

    public async Task ClearBackStack()
    {
      await CurrentApplication.MainPage.Navigation.PopToRootAsync();
    }

    public async Task NavigateBackAsync()
    {
      if (CurrentApplication.MainPage is MainView mainPage)
      {
        await mainPage.Detail.Navigation.PopAsync();
      }
      else if (CurrentApplication.MainPage != null)
      {
        await CurrentApplication.MainPage.Navigation.PopAsync();
      }
    }

    public virtual Task RemoveLastFromBackStackAsync()
    {
      if (CurrentApplication.MainPage is MainView mainPage)
      {
        mainPage.Detail.Navigation.RemovePage(
            mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
      }

      return Task.FromResult(true);
    }

    public async Task PopToRootAsync()
    {
      if (CurrentApplication.MainPage is MainView mainPage)
      {
        await mainPage.Detail.Navigation.PopToRootAsync();
      }
    }

    public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
    {
      return InternalNavigateToAsync(typeof(TViewModel), null);
    }

    public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
    {
      return InternalNavigateToAsync(typeof(TViewModel), parameter);
    }

    public Task NavigateToAsync(Type viewModelType)
    {
      return InternalNavigateToAsync(viewModelType, null);
    }

    public Task NavigateToAsync(Type viewModelType, object parameter)
    {
      return InternalNavigateToAsync(viewModelType, parameter);
    }

    protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
    {
      var page = CreatePage(viewModelType, parameter);

      //Set Master Page
      if (page is MainView)
      {
        Debug.WriteLine("page is MainView ln93");
        CurrentApplication.MainPage = page;
      }
      //Set Detail Page
      else if (CurrentApplication.MainPage is MainView)
      {
        Debug.WriteLine("CurrentApplication.MainPage is MainView ln99");
        var mainPage = CurrentApplication.MainPage as MainView;

        //It is not the first Detail
        if (mainPage.Detail is WhatsOnTheNavigationPage navigationPage)
        {
          Debug.WriteLine("mainPage.Detail is WhatsOnTheNavigationPage navigationPage ln105");
          var currentPage = navigationPage.CurrentPage;

          if (currentPage.GetType() != page.GetType())
          {
            Debug.WriteLine("currentPage.GetType() != page.GetType() ln110");
            await navigationPage.PushAsync(page);
          }
        }
        //It is the first Detail
        else
        {
          Debug.WriteLine("else ln117");
          navigationPage = new WhatsOnTheNavigationPage(page);
          mainPage.Detail = navigationPage;
        }
        Debug.WriteLine("end of elseif ln121");
        mainPage.IsPresented = false;
      }
      else
      {
        Debug.WriteLine("else ln126");
        var navigationPage = CurrentApplication.MainPage as WhatsOnTheNavigationPage;

        if (navigationPage != null)
        {
          Debug.WriteLine("navigationPage != null ln131");
          await navigationPage.PushAsync(page);
        }
        else
        {
          Debug.WriteLine("else navigationPage != null ln136");
          CurrentApplication.MainPage = new WhatsOnTheNavigationPage(page);
        }
      }

      Debug.WriteLine("end ln141");
      //Method that allows VM to Initialize Data
      await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
    }

    protected Type GetPageTypeForViewModel(Type viewModelType)
    {
      if (!_mappings.ContainsKey(viewModelType))
      {
        throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
      }

      return _mappings[viewModelType];
    }

    protected Page CreatePage(Type viewModelType, object parameter)
    {
      Type pageType = GetPageTypeForViewModel(viewModelType);

      if (pageType == null)
      {
        throw new Exception($"Mapping type for {viewModelType} is not a page");
      }

      try
      {
        Page page = Activator.CreateInstance(pageType) as Page;

        return page;
      }
      catch (Exception e)
      {
        throw e;
      }
      
    }

    private void CreatePageViewModelMappings()
    {
      _mappings.Add(typeof(MainViewModel), typeof(MainView));
      _mappings.Add(typeof(MenuViewModel), typeof(MenuView));
      _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
      _mappings.Add(typeof(ListItemsViewModel), typeof(ListItemsView));
      _mappings.Add(typeof(ItemDetailViewModel), typeof(ItemDetailView));
      _mappings.Add(typeof(NewItemViewModel), typeof(NewItemView));
    }
  }
}
