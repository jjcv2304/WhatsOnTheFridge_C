using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels.Base
{
  public class ViewModelBase: INotifyPropertyChanged
  {
    protected readonly INavigationService _navigationService;
    protected readonly IDialogService _dialogService;

    public ViewModelBase( INavigationService navigationService, 
      IDialogService dialogService)
    {
      _navigationService = navigationService;
      _dialogService = dialogService;
    }

    /// <summary>
    /// Called when page is appearing.
    /// </summary>
    public virtual void OnAppearing()
    {
      // No default implementation. 
    }

    /// <summary>
    /// Called when the view model is disappearing. View Model clean-up should be performed here.
    /// </summary>
    public virtual void OnDisappearing()
    {
      // No default implementation. 
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private bool _isBusy;
    public bool IsBusy
    {
      get => _isBusy;
      set
      {
        _isBusy = value;
        OnPropertyChanged(nameof(IsBusy));
      }
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual Task InitializeAsync(object data)
    {
      return Task.FromResult(false);
    }

  }
}
