using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatsOnTheFridge.Mobile.Core.Contracts.Services.General
{
  public interface IDialogService
  {
    Task ShowDialog(string message, string title, string buttonLabel);

    void ShowToast(string message);
  }
}
