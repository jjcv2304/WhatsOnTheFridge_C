using System;
using System.Collections.Generic;
using System.Text;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class ItemDetailViewModel: ViewModelBase
  {
    public ItemDetailViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
    {
    }
  }
}
