﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using ApprovalTests;
using ApprovalTests.Reporters;
using Moq;
using WhatsOnThe.Model;
using WhatsOnTheFridge.Core.Test.Fakes;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.Data;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.Services.Data;
using WhatsOnTheFridge.Mobile.Core.ViewModels;
using Xunit;
using WhatsOnTheFridge.Core.Test.ViewModelsApprovalTests.Extensions;
using System.Reactive.Linq;

namespace WhatsOnTheFridge.Core.Test.ViewModelsApprovalTests
{
  
  public class ListItemsViewModelApprovalTest
  {

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task AllItems_GetLoaded_AfterInitializeAsync()
    {
      var mockNavigationService = new Mock<INavigationService>();
      var mockDialogService = new Mock<IDialogService>();
      var mockItemsService = new Mock<IItemsService>();
      var fakeItemsList = Task.Run(() =>
        new List<Item>()
        {
          new Item() {Id = 1, Name = "Test 1", Description = "Description 12", Quantity = 1},
          new Item() {Id = 3, Name = "Test 3", Description = "Description 3", Quantity = 3}
        }
          .AsEnumerable()
      );
      mockItemsService.Setup(m => m.GetAllItemsAsync()).Returns(fakeItemsList);
      var listItemsViewModel = new ListItemsViewModel(mockNavigationService.Object, mockDialogService.Object, mockItemsService.Object);

      await listItemsViewModel.InitializeAsync(null);

      Approvals.Verify(listItemsViewModel.Items.ToList().ToApprovalString());
    }
  }
}