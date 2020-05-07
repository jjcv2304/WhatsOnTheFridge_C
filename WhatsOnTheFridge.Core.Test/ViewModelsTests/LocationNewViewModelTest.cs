using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using Moq;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Repositories;
using WhatsOnTheFridge.Mobile.Core.Services.Data;
using WhatsOnTheFridge.Mobile.Core.ViewModels;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;
using Xunit;

namespace WhatsOnTheFridge.Core.Test.ViewModelsTests
{
  public class LocationNewViewModelTest
  {
    [Fact]
    public void SaveItemCommand_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockLocationService = new Mock<ILocationsService>();

      var locationNewViewModel = new LocationNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockLocationService.Object);

      Assert.NotNull(locationNewViewModel.SaveLocationCommand);
    }

    [Fact]
    public void NewLocation_NotNull()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockLocationService = new Mock<ILocationsService>();

      var locationNewViewModel = new LocationNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockLocationService.Object);

      Assert.NotNull(locationNewViewModel.NewLocation);
    }
    
    [Fact]
    public void Navigate_IsCalled_WhenItemIsSaved()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockLocationService = new Mock<ILocationsService>();
      var locationNewViewModel = new LocationNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockLocationService.Object);

      locationNewViewModel.SaveLocationCommand.Execute(null);

      mockNavigationService.Verify(mock => mock.NavigateBackAsync(), Times.Once());
    }
    
    [Fact]
    public void AddLocationIsCalled_WhenItemIsSaved()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockLocationService = new Mock<ILocationsService>();
      var locationNewViewModel = new LocationNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockLocationService.Object);

      locationNewViewModel.SaveLocationCommand.Execute(null);

      mockLocationService.Verify(mock => mock.AddLocation(It.IsAny<Location>()), Times.Once());
    }
  }
}
