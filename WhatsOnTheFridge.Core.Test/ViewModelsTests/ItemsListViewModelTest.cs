using System.Threading.Tasks;
using Akavache;
using Moq;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Core.Test.Fakes;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Services.Data;
using WhatsOnTheFridge.Mobile.Core.ViewModels;
using Xunit;

namespace WhatsOnTheFridge.Core.Test.ViewModelsTests
{
  public class ItemsListViewModelTest
  {
    
    [Fact]
    public async Task Items_NotNull_AfterInitializeAsync()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
     
      var listItemsViewModel = new ItemsListViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      await listItemsViewModel.InitializeAsync(null);

      Assert.NotNull(listItemsViewModel.Items);
    }

    [Fact]
    public async Task AllItems_GetLoaded_AfterInitializeAsync()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsRepository = new FakeItemsRepository();
      var mockItemsService = new ItemsService(mockItemsRepository, new InMemoryBlobCache());
     
      var listItemsViewModel = new ItemsListViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);

      await listItemsViewModel.InitializeAsync(null);

      Assert.Equal(mockItemsRepository.Items.Count, listItemsViewModel.Items.Count);
    }

    [Fact]
    public void ItemTappedCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
     
      var listItemsViewModel = new ItemsListViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);
      
      Assert.NotNull(listItemsViewModel.ItemTappedCommand);
    }

    [Fact]
    public void NavigationIsCalled_WhenItemIsTapped()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var listItemsViewModel = new ItemsListViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);
      
      listItemsViewModel.ItemTappedCommand.Execute(It.IsAny<Item>());

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemDetailViewModel>(It.IsAny<Item>()), Times.Once());
    }

    [Fact]
    public void NavigationToItemDetailViewIsCalled_WithSelectedItem_WhenItemIsTapped()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var listItemsViewModel = new ItemsListViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);
    
      listItemsViewModel.ItemTappedCommand.Execute(It.IsAny<Item>());

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemDetailViewModel>(It.IsAny<Item>()), Times.Once());
    }


  }
}
