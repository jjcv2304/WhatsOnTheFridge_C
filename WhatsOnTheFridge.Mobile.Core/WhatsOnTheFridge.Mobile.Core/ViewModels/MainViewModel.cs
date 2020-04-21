using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
    private MenuViewModel _menuViewModel;

    public MainViewModel(INavigationService navigationService, IDialogService dialogService, MenuViewModel menuViewModel) : base(navigationService, dialogService)
    {
      _menuViewModel = menuViewModel;
    }
    public MenuViewModel MenuViewModel
    {
      get => _menuViewModel;
      set
      {
        _menuViewModel = value;
        OnPropertyChanged();
      }
    }

    public override async Task InitializeAsync(object data)
    {
      await Task.WhenAll
      (
        _menuViewModel.InitializeAsync(data),
        _navigationService.NavigateToAsync<HomeViewModel>()
      );
    }
  }
}
