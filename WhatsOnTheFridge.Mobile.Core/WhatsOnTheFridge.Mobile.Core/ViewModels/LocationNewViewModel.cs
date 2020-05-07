using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class LocationNewViewModel: ViewModelBase
  {
    private readonly ILocationsService _locationsService;

    public Location NewLocation { get; set; }

    public ICommand SaveLocationCommand => new Command(OnSaveLocation);
    
    public LocationNewViewModel(INavigationService navigationService, IDialogService dialogService, ILocationsService locationsService) : base(navigationService, dialogService)
    {
      NewLocation = GetInitializedNewLocation();
      _locationsService = locationsService;
    }

    private Location GetInitializedNewLocation()
    {
      return new Location()
      {
       Name = string.Empty,
       Description = string.Empty
      };
    }

    private async void OnSaveLocation(object obj)
    {
      await _locationsService.AddLocation(NewLocation);

      await _navigationService.NavigateBackAsync();
    }

  }
}
