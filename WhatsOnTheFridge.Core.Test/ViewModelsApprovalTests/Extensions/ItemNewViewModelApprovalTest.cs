using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using Moq;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Dto;
using WhatsOnTheFridge.Mobile.Core.ViewModels;
using Xamarin.Forms;
using Xunit;

namespace WhatsOnTheFridge.Core.Test.ViewModelsApprovalTests.Extensions
{
  public class ItemNewViewModelApprovalTest
  {
    private static async Task<ItemNewViewModel> MockFakeItemNewViewModel()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var mockLocationService = new Mock<ILocationsService>();

      var fakeItemsList = Task.Run(() =>
        new List<ItemSimpleDto>()
        {
          new ItemSimpleDto() {Id = 1, Name = "Test 1"},
          new ItemSimpleDto() {Id = 3, Name = "Test 3"},
          new ItemSimpleDto() {Id = 5, Name = "Test 5"},
          new ItemSimpleDto() {Id = 6, Name = "Test 6"},
          new ItemSimpleDto() {Id = 61, Name = "test 61"},
          new ItemSimpleDto() {Id = 500, Name = "TeBst 500"},
          new ItemSimpleDto() {Id = 600, Name = "TeBst 600"},
          new ItemSimpleDto() {Id = 10, Name = "BTest 10"},
          new ItemSimpleDto() {Id = 30, Name = "BTest 30"},
          new ItemSimpleDto() {Id = 50, Name = "BTest 50"},
          new ItemSimpleDto() {Id = 60, Name = "BTest 60"}
        }
      );

      mockItemsService.Setup(m => m.GetAllItemsNameAsync()).Returns(fakeItemsList);
      var itemNewViewModel =
        new ItemNewViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object, mockLocationService.Object);
      await itemNewViewModel.InitializeAsync(null);
      return itemNewViewModel;
    }

    [Fact]
    [UseReporter(typeof(TortoiseDiffReporter))]
    public async Task Suggestions_Update1_AfterUserInput()
    {
      var itemNewViewModel = await MockFakeItemNewViewModel();

      itemNewViewModel.NameChangedCommand.Execute(new TextChangedEventArgs("", "T"));

      Approvals.Verify(itemNewViewModel.Suggestions.ToList().ToApprovalString()); 
    }

    [Fact]
    [UseReporter(typeof(TortoiseDiffReporter))]
    public async Task Suggestions_Update2_AfterUserInput()
    {
      var itemNewViewModel = await MockFakeItemNewViewModel();

      itemNewViewModel.NameChangedCommand.Execute(new TextChangedEventArgs("", "t"));

      Approvals.Verify(itemNewViewModel.Suggestions.ToList().ToApprovalString()); 
    }
    
    [Fact]
    [UseReporter(typeof(TortoiseDiffReporter))]
    public async Task Suggestions_Update3_AfterUserInput()
    {
      var itemNewViewModel = await MockFakeItemNewViewModel();

      itemNewViewModel.NameChangedCommand.Execute(new TextChangedEventArgs("", "T"));
      itemNewViewModel.NameChangedCommand.Execute(new TextChangedEventArgs("T", "Tes"));

      Approvals.Verify(itemNewViewModel.Suggestions.ToList().ToApprovalString()); 
    }
        
    [Fact]
    [UseReporter(typeof(TortoiseDiffReporter))]
    public async Task Suggestions_Update4_AfterUserInput()
    {
      var itemNewViewModel = await MockFakeItemNewViewModel();

      itemNewViewModel.NameChangedCommand.Execute(new TextChangedEventArgs("", "T"));
      itemNewViewModel.NameChangedCommand.Execute(new TextChangedEventArgs("T", "Tes"));
      itemNewViewModel.NameChangedCommand.Execute(new TextChangedEventArgs("T", "Te"));

      Approvals.Verify(itemNewViewModel.Suggestions.ToList().ToApprovalString()); 
    }
  }
}
