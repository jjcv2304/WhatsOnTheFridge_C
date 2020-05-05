using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class HomeViewModel : ViewModelBase
  {
    public HomeViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
    {
    }

    public ICommand ViewAllItemsCommand => new Command(OnViewAllItems);
    public ICommand ViewAllLocationsCommand => new Command(OnViewAllLocations);
    public ICommand NewItemCommand => new Command(OnNewItem);
    public ICommand NewLocationCommand => new Command(OnNewLocation);
    public ICommand ViewAboutToExpireItemsCommand => new Command(OnViewAboutToExpireItems);
    public ICommand ViewFinishedItemsCommand => new Command(OnViewFinishedItems);
    public ICommand ViewExportItemsMenuCommand => new Command(OnViewExportItemsMenu);

    private async void OnViewAllItems()
    {
      await _navigationService.NavigateToAsync<ListItemsViewModel>();
    }
    private async void OnViewAllLocations()
    {
      throw new NotImplementedException();
    }
    private async void OnNewItem()
    {
      await _navigationService.NavigateToAsync<NewItemViewModel>();
    }
    private async void OnNewLocation()
    {
      throw new NotImplementedException();
    }
    private async void OnViewAboutToExpireItems()
    {
      throw new NotImplementedException();
    }
    private async void OnViewFinishedItems()
    {
      throw new NotImplementedException();
    }
    private async void OnViewExportItemsMenu()
    {
      throw new NotImplementedException();
    }


  }
}
