using System;
using System.Threading.Tasks;
using Akavache;
using Moq;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Core.Test.Builders;
using WhatsOnTheFridge.Core.Test.Fakes;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Dto;
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
      
      listItemsViewModel.ItemTappedCommand.Execute(ItemBuilder.Simple().Build());

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemDetailViewModel>(It.IsAny<Item>()), Times.Once());
    }

    [Fact]
    public void NavigationToItemDetailViewIsCalled_WithSelectedItem_WhenItemIsTapped()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var listItemsViewModel = new ItemsListViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);
    
      listItemsViewModel.ItemTappedCommand.Execute(ItemBuilder.Simple().Build());

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemDetailViewModel>(It.IsAny<Item>()), Times.Once());
    }

    [Fact]
    public async Task ItemsAboutToExpire_GetLoaded_WhenFilterIsAboutToExpire()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsRepository = new FakeItemsRepository();
      //Specify items ExpirationDate
      mockItemsRepository.Items.ForEach(i=>i.ExpirationDate = DateTime.Today.AddDays(15));
      mockItemsRepository.Items[0].ExpirationDate = DateTime.Today.AddDays(-1);
      mockItemsRepository.Items[1].ExpirationDate = DateTime.Today;
      mockItemsRepository.Items[2].ExpirationDate = DateTime.Today.AddDays(1);
      mockItemsRepository.Items[3].ExpirationDate = DateTime.Today.AddDays(4);
      var mockItemsService = new ItemsService(mockItemsRepository, new InMemoryBlobCache());
     
      var listItemsViewModel = new ItemsListViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);

      await listItemsViewModel.InitializeAsync(ItemListFilters.AboutToExpire);

      Assert.Equal(4, listItemsViewModel.Items.Count);
    }
  }
}
