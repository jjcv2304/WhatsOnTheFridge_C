using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using WhatsOnTheFridge.Mobile.Core.Constants;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class MenuViewModel : ViewModelBase
  {
    private ObservableCollection<MainMenuItemViewModel> _menuItems;

    public MenuViewModel(INavigationService navigationService, IDialogService dialogService)
        : base(navigationService, dialogService)
    {
      MenuItems = new ObservableCollection<MainMenuItemViewModel>();
      LoadMenuItems();
    }

    public string WelcomeText => "Hello Juan";

    public ICommand MenuItemTappedCommand => new Command(OnMenuItemTapped);

    public ObservableCollection<MainMenuItemViewModel> MenuItems
    {
      get => _menuItems;
      set
      {
        _menuItems = value;
        OnPropertyChanged();
      }
    }

    private void OnMenuItemTapped(object menuItemTappedEventArgs)
    {
      var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as MainMenuItemViewModel);

      //if (menuItem != null && menuItem.MenuText == "Log out")
      //{
      //    _navigationService.ClearBackStack();
      //}

      var type = menuItem?.ViewModelToLoad;
      _navigationService.NavigateToAsync(type);
    }

    private void LoadMenuItems()
    {
      MenuItems.Add(new MainMenuItemViewModel
      {
        MenuText = "Home",
        ViewModelToLoad = typeof(MainViewModel),
        MenuItemType = MenuItemType.Home
      });

      MenuItems.Add(new MainMenuItemViewModel
      {
        MenuText = "Items",
        ViewModelToLoad = typeof(ListItemsViewModel),
        MenuItemType = MenuItemType.Items
      });

    }
  }
}
