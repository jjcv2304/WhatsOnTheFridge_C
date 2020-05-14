﻿using System.Collections.ObjectModel;
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
  public class ItemsListViewModel: ViewModelBase
  {
    private readonly IItemsService _itemsService;
    private ObservableCollection<Item> _items;

    public ItemsListViewModel(INavigationService navigationService, IDialogService dialogService, IItemsService itemsService) : base(navigationService, dialogService)
    {
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
      var itemWithLocation = _itemsService.GetItemWithLocationAsync(selectedItem.Id).Result;
      _navigationService.NavigateToAsync<ItemDetailViewModel>(itemWithLocation);
    }

    public override async Task InitializeAsync(object data)
    {
      IsBusy = true;

      Items = (await _itemsService.GetAllItemsAsync()).ToObservableCollection();

      IsBusy = false;
    }
  }
}
