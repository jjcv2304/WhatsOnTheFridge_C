using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class ItemNewViewModel : ViewModelBase
  {
    private readonly IItemsService _itemsService;
    public Item NewITem { get; set; }

    public ICommand SaveItemCommand => new Command(OnSaveItem);

    public ItemNewViewModel(INavigationService navigationService, IDialogService dialogService, IItemsService itemsService) : base(navigationService, dialogService)
    {
      NewITem = GetInitializedNewItem();
      _itemsService = itemsService;
    }

    public override async Task InitializeAsync(object item)
    {

      // TODO Dictionary<string, id> =   availableItems = GetAllNames 
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


  }
}
