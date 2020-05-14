using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Dto;
using WhatsOnTheFridge.Mobile.Core.Extensions;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class ItemNewViewModel : ViewModelBase
  {
    private readonly IItemsService _itemsService;
    private readonly ILocationsService _locationsService;
    private ObservableCollection<ItemSimpleDto> _suggestions;
    private ObservableCollection<LocationSimpleDto> _locations;
    private List<ItemSimpleDto> _allSuggestions;
    private bool _suggestionsAreVisible;
    private int _suggestionsHeightRequest;

    public Item NewITem { get; set; }
    public ObservableCollection<ItemSimpleDto> Suggestions
    {
      get => _suggestions;
      set
      {
        _suggestions = value;
        OnPropertyChanged();
      }
    }
    public ObservableCollection<LocationSimpleDto> Locations
    {
      get => _locations;
      set
      {
        _locations = value;
        OnPropertyChanged();
      }
    }
    public bool SuggestionsAreVisible
    {
      get => _suggestionsAreVisible;
      set
      {
        _suggestionsAreVisible = value;
        OnPropertyChanged();
      }
    }
    public int SuggestionsHeightRequest
    {
      get => _suggestionsHeightRequest;
      set
      {
        _suggestionsHeightRequest = value;
        OnPropertyChanged();
      }
    }

    public ICommand SaveItemCommand => new Command(OnSaveItem);
    public ICommand NameChangedCommand => new Command<TextChangedEventArgs>(OnNameChangedCommand);
    public ICommand ItemTappedCommand => new Command<ItemSimpleDto>(OnItemTapped);
    public ICommand LocationTappedCommand => new Command<LocationSimpleDto>(OnLocationTapped);

    public ItemNewViewModel(INavigationService navigationService, IDialogService dialogService, IItemsService itemsService, ILocationsService locationsService) : base(navigationService, dialogService)
    {
      NewITem = GetInitializedNewItem();
      _itemsService = itemsService;
      _locationsService = locationsService;
    }

    public override async Task InitializeAsync(object item)
    {
      IsBusy = true;

      _allSuggestions = await _itemsService.GetAllItemsNameAsync();
      Suggestions = _allSuggestions.ToObservableCollection();

      var locations = await _locationsService.GetAllLocationsNameAsync();
      Locations = locations.ToObservableCollection();

      IsBusy = false;
    }

    private Item GetInitializedNewItem()
    {
      return new Item()
      {
        Quantity = 0,
        ExpirationDate = DateTime.Today.AddDays(7),
        AddedDate = DateTime.Today
      };
    }

    private async void OnSaveItem()
    {
      await _itemsService.AddItem(NewITem);

      await _navigationService.NavigateToAsync<HomeViewModel>();

    }

    private void OnNameChangedCommand(TextChangedEventArgs e)
    {
      if (e.NewTextValue == null || Suggestions == null) return;

      if (_allSuggestions.Any(x => x.Name.StartsWith(e.NewTextValue)) && e.NewTextValue != string.Empty)
      {
        var items = _allSuggestions.Where(x => x.Name.StartsWith(e.NewTextValue)).ToList();
        SuggestionsAreVisible = true;
        Suggestions = items.ToObservableCollection();
        SuggestionsHeightRequest = (items.Count + 1) * 23;
      }
      else
      {
        SuggestionsAreVisible = false;
        Suggestions = _allSuggestions.ToObservableCollection();
      }
    }
    private async void OnItemTapped(ItemSimpleDto selectedItem)
    {
      var item = await _itemsService.GetItemAsync(selectedItem.Id);
      await _navigationService.NavigateToAsync<ItemDetailViewModel>(item);
      await _navigationService.RemoveLastFromBackStackAsync();
    }
    private void OnLocationTapped(LocationSimpleDto selectedLocation)
    {
      NewITem.LocationId = selectedLocation.Id;
    }
  }
}
