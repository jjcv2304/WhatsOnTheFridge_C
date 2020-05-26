using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
  public class ItemsListGroupByLocationViewModel : ViewModelBase
  {
    private readonly IItemsService _itemsService;
    private ObservableCollection<ItemsByLocationGrouping> _itemsByLocations;

    public ItemsListGroupByLocationViewModel(INavigationService navigationService, IDialogService dialogService, IItemsService itemsService) : base(navigationService, dialogService)
    {
      _itemsService = itemsService;
    }

    public ICommand ItemTappedCommand => new Command<Item>(OnItemTapped);

    public ObservableCollection<ItemsByLocationGrouping> ItemsByLocation
    {
      get => _itemsByLocations;
      set
      {
        _itemsByLocations = value;
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

      var items = await _itemsService.GetAllItemsAsync();
      var locations = ItemsGroupByLocations(items);
      ItemsByLocation = locations;

      IsBusy = false;
    }

    private static ObservableCollection<ItemsByLocationGrouping> ItemsGroupByLocations(IEnumerable<Item> items)
    {
      var itemsList = items.ToList();
    
      var locations = itemsList
        .Where(i => i.Location != null)
        .GroupBy(i => i.Location.Id)
        .Select(i => new ItemsByLocationGrouping(i.First().Location.Id, i.First().Location.Name))
        .ToObservableCollection();

      foreach (var location in locations)
      {
        foreach (var item in itemsList.Where(i => i.Location != null && i.Location.Id == location.Id))
        {
          location.Add(item);
        }
      }

      return locations;
    }
  }
}
