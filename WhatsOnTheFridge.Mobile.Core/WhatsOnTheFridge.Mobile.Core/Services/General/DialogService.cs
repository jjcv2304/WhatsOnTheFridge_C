using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;

namespace WhatsOnTheFridge.Mobile.Core.Services.General
{
  public class DialogService: IDialogService
  {
    public Task ShowDialog(string message, string title, string buttonLabel)
    {
      return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
    }

    public void ShowToast(string message)
    {
      UserDialogs.Instance.Toast(message);
    }
  }
}
