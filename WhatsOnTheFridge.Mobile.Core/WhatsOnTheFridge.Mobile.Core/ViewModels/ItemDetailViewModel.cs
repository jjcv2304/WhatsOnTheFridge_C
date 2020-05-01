using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Constants;
using WhatsOnTheFridge.Mobile.Core.Contracts.Repositories;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Repositories;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class ItemDetailViewModel : ViewModelBase
  {
    private readonly IItemsService _itemsService;
    private Item _selectedItem;

    public Item SelectedItem
    {
      get => _selectedItem;
      set
      {
        _selectedItem = value;
        OnPropertyChanged();
      }
    }

    public ICommand ModifyQuantityCommand => new Command<ValueChangedEventArgs>(OnModifyQuantity);
    public ICommand ModifyItemCommand => new Command(OnModifyItem);

    public ItemDetailViewModel(INavigationService navigationService, IDialogService dialogService, IItemsService itemsService) : base(navigationService, dialogService)
    {
      _itemsService = itemsService;
    }

    public override async Task InitializeAsync(object item)
    {
      SelectedItem = (Item)item;
    }

    public async void OnModifyItem()
    {
      await _itemsService.ModifyItem(SelectedItem);
      // await _dialogService.ShowDialog("Item modified", "Success", "OK");

      await _navigationService.NavigateToAsync<ListItemsViewModel>();

      //Messages only for VM to VM comunitcation
      //MessagingCenter.Send(this, MessagingConstants.ModifiedItem, SelectedItem);
      //await _dialogService.ShowDialog("Item modified", "Success", "OK");
    }

    private void OnModifyQuantity(ValueChangedEventArgs e)
    {
      SelectedItem.Quantity = (int)e.NewValue;
      OnPropertyChanged(nameof(SelectedItem));
    }
  }
}
