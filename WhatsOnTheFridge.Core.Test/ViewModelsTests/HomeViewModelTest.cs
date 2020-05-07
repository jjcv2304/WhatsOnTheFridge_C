using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels;
using Xunit;

namespace WhatsOnTheFridge.Core.Test.ViewModelsTests
{
  public class HomeViewModelTest
  {
    [Fact]
    public void ViewAllItemsCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
     
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);
      
      Assert.NotNull(homeViewModel.ViewAllItemsCommand);
    }
    [Fact]
    public void ViewAllLocationsCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
     
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);
      
      Assert.NotNull(homeViewModel.ViewAllLocationsCommand);
    }
    [Fact]
    public void ViewAboutToExpireItemsCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
     
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);
      
      Assert.NotNull(homeViewModel.ViewAboutToExpireItemsCommand);
    }
    [Fact]
    public void ViewExportItemsMenuCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
     
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);
      
      Assert.NotNull(homeViewModel.ViewExportItemsMenuCommand);
    }
    [Fact]
    public void ViewFinishedItemsCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
     
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);
      
      Assert.NotNull(homeViewModel.ViewFinishedItemsCommand);
    }
    [Fact]
    public void NewItemCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
     
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);
      
      Assert.NotNull(homeViewModel.NewItemCommand);
    }
    [Fact]
    public void NewLocationCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
     
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);
      
      Assert.NotNull(homeViewModel.NewLocationCommand);
    }

    [Fact]
    public void Navigation_ToNewItem_WhenNewItemClicked()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);

      homeViewModel.NewItemCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemNewViewModel>(), Times.Once());
    }

    [Fact]
    public void Navigation_ToNewLocation_WhenNewLocationClicked()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);

      homeViewModel.NewLocationCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<LocationNewViewModel>(), Times.Once());
    }

    [Fact]
    public void Navigation_ToAllItems_WhenViewItemsClicked()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var homeViewModel = new HomeViewModel(mockNavigationService.Object, mockDialogService.Object);

      homeViewModel.ViewAllItemsCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ItemsListViewModel>(), Times.Once());
    }
  }
}
