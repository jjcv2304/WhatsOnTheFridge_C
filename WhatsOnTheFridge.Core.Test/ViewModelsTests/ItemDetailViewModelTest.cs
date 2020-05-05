using System;
using System.Threading.Tasks;
using Akavache;
using Moq;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Core.Test.Fakes;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Services.Data;
using WhatsOnTheFridge.Mobile.Core.ViewModels;
using Xamarin.Forms;
using Xunit;
using ItemBuilder = WhatsOnTheFridge.Core.Test.Builders.ItemBuilder;

namespace WhatsOnTheFridge.Core.Test.ViewModelsTests
{
  public class ItemDetailViewModelTest
  {
    [Fact]
    public async Task SelectedItem_NotNull_AfterInitializeAsync()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new ItemsService(new FakeItemsRepository(), new InMemoryBlobCache());
      var itemDetailViewModel = new ItemDetailViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);

      await itemDetailViewModel.InitializeAsync(ItemBuilder.Typical().Build());

      Assert.NotNull(itemDetailViewModel.SelectedItem);
    }

    [Fact]
    public void ModifyItemCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemDetailViewModel = new ItemDetailViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      Assert.NotNull(itemDetailViewModel.ModifyItemCommand);
    }

    [Fact]
    public void ModifyQuantityCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemDetailViewModel = new ItemDetailViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      Assert.NotNull(itemDetailViewModel.ModifyQuantityCommand);
    }

    [Fact]
    public void PropertyChanged_IsCalled_WhenItemQuantityIsChanged()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemDetailViewModel =
        new ItemDetailViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object)
        {
          SelectedItem = ItemBuilder.TypicalWId().Build()
        };
      //subscribe
      var parameterChangedName = string.Empty;
      itemDetailViewModel.PropertyChanged += (sender, e) => parameterChangedName = e.PropertyName;

      itemDetailViewModel.ModifyQuantityCommand.Execute(new ValueChangedEventArgs(GetRandom.Int16(), GetRandom.Int16()));

      Assert.Equal(nameof(itemDetailViewModel.SelectedItem), parameterChangedName);
    }

    [Fact]
    public void SelectedItemQuantity_IsUpdated_WhenItemQuantityIsChanged()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemDetailViewModel =
        new ItemDetailViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object)
        {
          SelectedItem = ItemBuilder.TypicalWId().Build()
        };

      //the new value will be the existing value + or - a random number . 
      var currValue = itemDetailViewModel.SelectedItem.Quantity;
      var newValue = currValue + GetRandom.Int32(-currValue, 100);//-currValue to guarantee that we dont have less than 0
      itemDetailViewModel.ModifyQuantityCommand.Execute(new ValueChangedEventArgs(currValue, newValue));

      Assert.Equal(newValue, itemDetailViewModel.SelectedItem.Quantity);
    }

    [Fact]
    public void SelectedItemQuantity_CanNotBe_LessThanZero()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemDetailViewModel =
        new ItemDetailViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object)
        {
          SelectedItem = ItemBuilder.TypicalWId().Build()
        };

      var currValue = itemDetailViewModel.SelectedItem.Quantity;
      var newValue = GetRandom.Int32(-100, -1);

      Assert.Throws<ArgumentOutOfRangeException>(() 
        => itemDetailViewModel.ModifyQuantityCommand.Execute(new ValueChangedEventArgs(currValue, newValue)));
    }

    [Fact]
    public void NavigationToListItem_IsCalled_WhenItemIsModified()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemDetailViewModel = new ItemDetailViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      itemDetailViewModel.ModifyItemCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemsListViewModel>(), Times.Once());
    }

    [Fact]
    public void ItemServiceModifyItemM_IsCalled_WhenItemIsModified()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemDetailViewModel = new ItemDetailViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      itemDetailViewModel.ModifyItemCommand.Execute(null);

      mockItemsService.Verify(mock => mock.ModifyItem(It.IsAny<Item>()), Times.Once());
    }
  }
}
