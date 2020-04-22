using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Repositories;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class ItemDetailViewModel : ViewModelBase
  {
    private readonly ItemsRepository _itemsRepository;
    private Item _selectedItem;

    public ItemDetailViewModel(INavigationService navigationService, IDialogService dialogService, ItemsRepository itemsRepository) : base(navigationService, dialogService)
    {
      _itemsRepository = itemsRepository;
    }

    public override async Task InitializeAsync(object item)
    {
      SelectedItem = (Item)item;
    }

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

    private void OnModifyQuantity(ValueChangedEventArgs e)
    {
      SelectedItem.Quantity = (int)e.NewValue;
      OnPropertyChanged(nameof(SelectedItem));
    }

    public ICommand ModifyItem => new Command(OnModifyItem);

    public async void OnModifyItem()
    {
      await _itemsRepository.SaveItemAsync(SelectedItem);

      //Messages only for VM to VM comunitcation
      //MessagingCenter.Send(this, MessagingConstants.AddPieToBasket, SelectedPie);
      //await _dialogService.ShowDialog("Pie added to your cart", "Success", "OK");
    }
  }
}
