using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using Moq;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Core.Test.Mocks;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Services.Data;
using WhatsOnTheFridge.Mobile.Core.ViewModels;
using Xunit;

namespace WhatsOnTheFridge.Core.Test.ViewModels
{
  public class ListItemsViewModelTest
  {
    
    [Fact]
    public async Task Items_NotNull_AfterInitializeAsync()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new ItemsService(new MockItemsRepository(), new InMemoryBlobCache());
     
      var listItemsViewModel = new ListItemsViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);

      await listItemsViewModel.InitializeAsync(null);

      Assert.NotNull(listItemsViewModel.Items);
    }

    [Fact]
    public async Task AllItems_GetLoaded_AfterInitializeAsync()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsRepository = new MockItemsRepository();
      var mockItemsService = new ItemsService(mockItemsRepository, new InMemoryBlobCache());
     
      var listItemsViewModel = new ListItemsViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);

      await listItemsViewModel.InitializeAsync(null);

      Assert.Equal(mockItemsRepository.Items.Count, listItemsViewModel.Items.Count);
    }

    [Fact]
    public void ItemTappedCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsRepository = new MockItemsRepository();
      var mockItemsService = new ItemsService(mockItemsRepository, new InMemoryBlobCache());
     
      var listItemsViewModel = new ListItemsViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);
      
      Assert.NotNull(listItemsViewModel.ItemTappedCommand);
    }

    [Fact]
    public void NavigationIsCalled_WhenItemIsTapped()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsRepository = new MockItemsRepository();
      var mockItemsService = new ItemsService(mockItemsRepository, new InMemoryBlobCache());
      var listItemsViewModel = new ListItemsViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);
      
      listItemsViewModel.ItemTappedCommand.Execute(It.IsAny<Item>());

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemDetailViewModel>(It.IsAny<Item>()), Times.Once());
    }

    [Fact]
    public void NavigationIsCalled_WithSelectedItem_WhenItemIsTapped()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsRepository = new MockItemsRepository();
      var mockItemsService = new ItemsService(mockItemsRepository, new InMemoryBlobCache());
      var listItemsViewModel = new ListItemsViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);
    
      var selectedItem = mockItemsRepository.Items.First();
      listItemsViewModel.ItemTappedCommand.Execute(selectedItem);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemDetailViewModel>(selectedItem), Times.Once());
    }


  }
}
