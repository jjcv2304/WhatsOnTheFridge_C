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
  public class ItemNewViewModel: ViewModelBase
  {
    private readonly IItemsService _itemsService;
    public Item NewITem { get; set; }

    public ICommand SaveItemCommand => new Command(OnSaveItem);

    public ItemNewViewModel(INavigationService navigationService, IDialogService dialogService, IItemsService itemsService) : base(navigationService, dialogService)
    {
      _itemsService = itemsService;
    }

    public override async Task InitializeAsync(object item)
    {
     // TODO Dictionary<string, id> =   availableItems = GetAllNames 
    }

    private async void OnSaveItem()
    {
      await _itemsService.AddItem(NewITem);

      await _navigationService.NavigateToAsync<HomeViewModel>();

    }

    
  }
}
