using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using WhatsOnTheFridge.Mobile.Core.ViewModels.Base;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class HomeViewModel: ViewModelBase
  {
    public HomeViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
    {
    }
    public override async Task InitializeAsync(object data)
    {
      //PiesOfTheWeek = (await _catalogDataService.GetPiesOfTheWeekAsync()).ToObservableCollection();
    }
  }
}
