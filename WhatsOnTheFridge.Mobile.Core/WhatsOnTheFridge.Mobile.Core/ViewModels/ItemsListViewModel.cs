using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Constants;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Dto;
using WhatsOnTheFridge.Mobile.Core.Extensions;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xamarin.Forms;
namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class ItemsListViewModel : ViewModelBase
  {
    private readonly IItemsService _itemsService;
    private ObservableCollection<Item> _items;
    private List<Item> _fullItemsList;
    private string _filter;
    private string _secondaryFilter;

    string _pageTitle { get; set; }
    public ItemsListViewModel(INavigationService navigationService, IDialogService dialogService, IItemsService itemsService) : base(navigationService, dialogService)
    {
      _itemsService = itemsService;
    }

    public ICommand ItemTappedCommand => new Command<Item>(OnItemTapped);

    public ICommand PerformSearchCommand => new Command<TextChangedEventArgs>(PerformSearch);

    public void PerformSearch(TextChangedEventArgs args)
    {
      if (string.IsNullOrEmpty(args.NewTextValue))
      {
        Items = _fullItemsList.ToObservableCollection();
      }
      else
      {
        Items = _fullItemsList.Where(i => i.Name.ToLower().Contains(args.NewTextValue.ToLower())).ToObservableCollection();
      }
    }

    public ObservableCollection<Item> Items
    {
      get => _items;
      set
      {
        _items = value;
        OnPropertyChanged();
      }
    }

    public string PageTitle
    {
      get => _pageTitle;
      set
      {
        _pageTitle = value;
        OnPropertyChanged();
      }
    }

    private void OnItemTapped(Item selectedItem)
    {
      var itemWithLocation = _itemsService.GetItemWithLocationAsync(selectedItem.Id).Result;
      _navigationService.NavigateToAsync<ItemDetailViewModel>(itemWithLocation);
    }

    public override async Task InitializeAsync(object filter)
    {
      IsBusy = true;

      _filter = filter?.ToString();
      await LoadItems();
      IsBusy = false;
    }

    private async Task LoadItems()
    {
      if (_filter == ItemListFilters.AboutToExpire)
      {
        PageTitle = "Items about to expire";
        _fullItemsList = (await _itemsService.GetAllItemsAsync())
          .Where(i => i.ExpirationDate < DateTime.Now.AddDays(7) && i.ExpirationDate > DateTime.Now.AddDays(-5))
          .ToList();
      }
      else
      {
        PageTitle = "All Items";
        _fullItemsList = (await _itemsService.GetAllItemsAsync()).ToList();
      }

      Items = _fullItemsList.ToObservableCollection();
    }
    /*
     *    private async Task LoadItems()
    {
      List<Item> tmpItems;
      if (_filter == ItemListFilters.AboutToExpire)
      {
        PageTitle = "Items about to expire";
        tmpItems = (await _itemsService.GetAllItemsAsync())
          .Where(i => i.ExpirationDate < DateTime.Now.AddDays(7) && i.ExpirationDate > DateTime.Now.AddDays(-5))
          .ToList();
      }
      else
      {
        PageTitle = "All Items";
        tmpItems = (await _itemsService.GetAllItemsAsync()).ToList();
      }

      if (!String.IsNullOrEmpty(_secondaryFilter))
      {
        tmpItems = tmpItems.Where(i => _secondaryFilter.Contains(i.Name)).ToList();
      }

      Items = tmpItems.ToObservableCollection();
    }
     *
     */

    public override void OnAppearing()
    {
      base.OnAppearing();
      MessagingCenter.Subscribe<ItemDetailViewModel>(this, MessagingConstants.ModifiedItem, async (sender) =>
     {
       await LoadItems();
     });
    }

    public override void OnDisappearing()
    {
      base.OnDisappearing();
      MessagingCenter.Unsubscribe<ItemDetailViewModel>(this, MessagingConstants.ModifiedItem);
    }
  }
}
