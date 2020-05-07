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
    [Fact]
    public void SaveItemCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();

      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      Assert.NotNull(itemNewViewModel.SaveItemCommand);
    }

    [Fact]
    public void NameChangedCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();

      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      Assert.NotNull(itemNewViewModel.NameChangedCommand);
    }

    [Fact]
    public void ItemTappedCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();

      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      Assert.NotNull(itemNewViewModel.ItemTappedCommand);
    }

    [Fact]
    public void Navigate_IsCalled_WhenItemIsSaved()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      itemNewViewModel.SaveItemCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ViewModelBase>(), Times.Once());
    }

    [Fact]
    public void NavigateToHomeView_IsCalled_WhenItemIsSaved()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      itemNewViewModel.SaveItemCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<HomeViewModel>(), Times.Once());
    }

    [Fact]
    public void NavigateToItemDetail_IsCalled_WhenSuggestionIsTapped()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      mockItemsService.Setup(m => m.GetItemAsync(It.IsAny<int>())).Returns(Task.FromResult(ItemBuilder.Simple().Build()));
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      itemNewViewModel.ItemTappedCommand.Execute(new ItemSimpleDto());

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemDetailViewModel>(It.IsAny<Item>()), Times.Once());
    }

    [Fact]
    public void RemoveLastFromBackStackAsync_IsCalled_WhenSuggestionIsTapped()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      mockItemsService.Setup(m => m.GetItemAsync(It.IsAny<int>())).Returns(Task.FromResult(ItemBuilder.Simple().Build()));
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      itemNewViewModel.ItemTappedCommand.Execute(new ItemSimpleDto());

      mockNavigationService.Verify(mock => mock.RemoveLastFromBackStackAsync(), Times.Once());
    }

    [Fact]
    public async Task AllItems_GetLoaded_AfterInitializeAsync()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsRepository = new FakeItemsRepository();
      var mockItemsService = new ItemsService(mockItemsRepository, new InMemoryBlobCache());
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);

      await itemNewViewModel.InitializeAsync(null);

      Assert.Equal(mockItemsRepository.Items.Count, itemNewViewModel.Suggestions.Count);
    }

    [Fact]
    public async Task AllItemsAreInsideSuggestions_WhenSuggestionIsTapped()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsRepository = new FakeItemsRepository();
      var mockItemsService = new ItemsService(mockItemsRepository, new InMemoryBlobCache());
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);
      await itemNewViewModel.InitializeAsync(null);

      itemNewViewModel.ItemTappedCommand.Execute(null);

      Assert.Equal(mockItemsRepository.Items.Count, itemNewViewModel.Suggestions.Count);
    }


    [Fact]
    public void AddItemIsCalled_WhenItemIsSaved()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      itemNewViewModel.SaveItemCommand.Execute(null);

      mockItemsService.Verify(mock => mock.AddItem(It.IsAny<Item>()), Times.Once());
    }

  }
}
