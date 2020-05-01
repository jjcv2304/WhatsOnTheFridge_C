using System;
using System.Collections.Generic;
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
  public class ItemDetailViewModelTest
  {
    [Fact]
    public async Task SelectedItem_NotNull_AfterInitializeAsync()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new ItemsService(new MockItemsRepository(), new InMemoryBlobCache());
      var itemDetailViewModel = new ItemDetailViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService);

    //  await itemDetailViewModel.InitializeAsync();

      Assert.NotNull(itemDetailViewModel.SelectedItem);
    }
  }
}
