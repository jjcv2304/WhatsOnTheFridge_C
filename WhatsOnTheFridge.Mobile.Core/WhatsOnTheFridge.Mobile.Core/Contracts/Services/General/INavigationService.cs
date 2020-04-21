using System;
using System.Threading.Tasks;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;

namespace WhatsOnTheFridge.Mobile.Core.Contracts.Services.General
{
  public interface INavigationService
  {
    Task InitializeAsync();
    Task ClearBackStack();
    Task NavigateBackAsync();
    Task RemoveLastFromBackStackAsync();
    Task PopToRootAsync();
    Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
    Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
    Task NavigateToAsync(Type viewModelType);
    Task NavigateToAsync(Type viewModelType, object parameter);
  }
}