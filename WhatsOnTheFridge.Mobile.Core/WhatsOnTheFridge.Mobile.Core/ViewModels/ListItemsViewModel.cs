using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Extensions;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class ListItemsViewModel: ViewModelBase
  {
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private readonly IItemsService _itemsService;
    private ObservableCollection<Item> _items;

    public ListItemsViewModel(INavigationService navigationService, IDialogService dialogService, IItemsService itemsService) : base(navigationService, dialogService)
    {
      _navigationService = navigationService;
      _dialogService = dialogService;
      _itemsService = itemsService;
    }
    
    public ICommand ItemTappedCommand => new Command<Item>(OnItemTapped);
    public ObservableCollection<Item> Items 
    {
      get => _items;
      set
      {
        _items = value;
        OnPropertyChanged();
      }
    }

    private void OnItemTapped(Item selectedItem)
    {
      _navigationService.NavigateToAsync<ItemDetailViewModel>(selectedItem);
    }
    
    public override async Task InitializeAsync(object data)
    {
      IsBusy = true;

      Items = (await _itemsService.GetAllItemsAsync()).ToObservableCollection();

      IsBusy = false;
    }
  }
}
