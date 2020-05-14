using System;
using System.Collections.Generic;
using System.Text;
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
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xunit;

namespace WhatsOnTheFridge.Core.Test.ViewModelsTests
{
  public class ItemNewViewModelTest
  {

    private static ItemNewViewModel ItemNewViewModel_WithMockDependencies()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var mockLocationService = new Mock<ILocationsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object, mockLocationService.Object);
      return itemNewViewModel;
    }
    private static ItemNewViewModel ItemNewViewModel_WithMockDependencies(out Mock<INavigationService> mockNavigationService)
    {
      mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var mockLocationService = new Mock<ILocationsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object, mockLocationService.Object);
      return itemNewViewModel;
    }
    private static ItemNewViewModel ItemNewViewModel_WithMockDependencies(out Mock<IItemsService> mockItemsService)
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      mockItemsService = new Mock<IItemsService>();
      var mockLocationService = new Mock<ILocationsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object, mockLocationService.Object);
      return itemNewViewModel;
    }
    private static ItemNewViewModel ItemNewViewModel_WithMockDependencies(out Mock<INavigationService> mockNavigationService, Mock<IItemsService> mockItemsService)
    {
      mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockLocationService = new Mock<ILocationsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object, mockLocationService.Object);
      return itemNewViewModel;
    }
    private static ItemNewViewModel ItemNewViewModel_WithMockDependencies_And_FakeRepository(out FakeItemsRepository mockItemsRepository)
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      mockItemsRepository = new FakeItemsRepository();
      var mockItemsService = new ItemsService(mockItemsRepository, new InMemoryBlobCache());
      var mockLocationService = new Mock<ILocationsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService, mockLocationService.Object);
      return itemNewViewModel;
    }
    private static ItemNewViewModel ItemNewViewModel_WithMockDependencies_And_FakeRepository(out FakeLocationsRepository mockLocationsRepository)
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();

      mockLocationsRepository = new FakeLocationsRepository();
      var mockLocationService = new LocationsService(mockLocationsRepository, new InMemoryBlobCache());

      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object, mockLocationService);

      return itemNewViewModel;
    }

    [Fact]
    public void SaveItemCommand_NotNull()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies();

      Assert.NotNull(itemNewViewModel.SaveItemCommand);
    }
    [Fact]
    public void NameChangedCommand_NotNull()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies();

      Assert.NotNull(itemNewViewModel.NameChangedCommand);
    }
    [Fact]
    public void ItemTappedCommand_NotNull()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies();

      Assert.NotNull(itemNewViewModel.ItemTappedCommand);
    }
    [Fact]
    public void LocationTappedCommand_NotNull()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies();

      Assert.NotNull(itemNewViewModel.LocationTappedCommand);
    }
    [Fact]
    public void Navigate_IsCalled_WhenItemIsSaved()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies(out Mock<INavigationService> mockNavigationService);

      itemNewViewModel.SaveItemCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ViewModelBase>(), Times.Once());
    }
    [Fact]
    public void NavigateToHomeView_IsCalled_WhenItemIsSaved()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies(out Mock<INavigationService> mockNavigationService);

      itemNewViewModel.SaveItemCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<HomeViewModel>(), Times.Once());
    }
    [Fact]
    public void NavigateToItemDetail_IsCalled_WhenSuggestionIsTapped()
    {
      var mockItemsService = new Mock<IItemsService>();
      mockItemsService.Setup(m => m.GetItemAsync(It.IsAny<int>())).Returns(Task.FromResult(ItemBuilder.Simple().Build()));
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies(out var mockNavigationService, mockItemsService);

      itemNewViewModel.ItemTappedCommand.Execute(new ItemSimpleDto());

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemDetailViewModel>(It.IsAny<Item>()), Times.Once());
    }
    [Fact]
    public void RemoveLastFromBackStackAsync_IsCalled_WhenSuggestionIsTapped()
    {
      var mockItemsService = new Mock<IItemsService>();
      mockItemsService.Setup(m => m.GetItemAsync(It.IsAny<int>())).Returns(Task.FromResult(ItemBuilder.Simple().Build()));
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies(out var mockNavigationService, mockItemsService);

      itemNewViewModel.ItemTappedCommand.Execute(new ItemSimpleDto());

      mockNavigationService.Verify(mock => mock.RemoveLastFromBackStackAsync(), Times.Once());
    }
    [Fact]
    public async Task AllItems_GetLoaded_AfterInitializeAsync()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies_And_FakeRepository(out FakeItemsRepository mockItemsRepository);

      await itemNewViewModel.InitializeAsync(null);

      Assert.Equal(mockItemsRepository.Items.Count, itemNewViewModel.Suggestions.Count);
    }
    [Fact]
    public async Task AllItemsAreInsideSuggestions_WhenSuggestionIsTapped()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies_And_FakeRepository(out FakeItemsRepository mockItemsRepository);
      await itemNewViewModel.InitializeAsync(null);

      itemNewViewModel.ItemTappedCommand.Execute(null);

      Assert.Equal(mockItemsRepository.Items.Count, itemNewViewModel.Suggestions.Count);
    }
    [Fact]
    public void AddItemIsCalled_WhenItemIsSaved()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies(out Mock<IItemsService> mockItemsService);

      itemNewViewModel.SaveItemCommand.Execute(null);

      mockItemsService.Verify(mock => mock.AddItem(It.IsAny<Item>()), Times.Once());
    }
    [Fact]
    public async Task AllLocationsAreInsideDropdown_AfterInitializeAsync()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies_And_FakeRepository(out FakeLocationsRepository mockLocationsRepository);

      await itemNewViewModel.InitializeAsync(null);

      Assert.Equal(mockLocationsRepository._locations.Count, itemNewViewModel.Locations.Count);
    }
    [Fact]
    public async Task NewItemLocation_IsSet_WhenLocationIsTapped()
    {
      var itemNewViewModel = ItemNewViewModel_WithMockDependencies_And_FakeRepository(out FakeLocationsRepository mockLocationsRepository);
      await itemNewViewModel.InitializeAsync(null);

      var tappedLocation = new LocationSimpleDto() { Id = GetRandom.Id() };
      itemNewViewModel.LocationTappedCommand.Execute(tappedLocation);

      Assert.Equal(tappedLocation.Id, itemNewViewModel.NewITem.LocationId);

    }

  }
}
