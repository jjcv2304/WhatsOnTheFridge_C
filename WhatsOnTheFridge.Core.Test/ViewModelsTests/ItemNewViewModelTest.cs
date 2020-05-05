using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xunit;

namespace WhatsOnTheFridge.Core.Test.ViewModelsTests
{
  public class ItemNewViewModelTest
  {
    [Fact]
    public void ItemTappedCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
     
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);
      
      Assert.NotNull(itemNewViewModel.SaveItemCommand);
    }

    [Fact]
    public void NavigateIsCalled_WhenItemIsSaved()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);
    
      itemNewViewModel.SaveItemCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<ViewModelBase>(), Times.Once());
    }

    [Fact]
    public void NavigateToHomeViewIsCalled_WhenItemIsSaved()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var itemNewViewModel = new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);
    
      itemNewViewModel.SaveItemCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateToAsync<HomeViewModel>(), Times.Once());
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
