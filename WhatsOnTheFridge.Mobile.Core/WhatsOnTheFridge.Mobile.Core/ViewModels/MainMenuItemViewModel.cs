using System;
using System.Collections.Generic;
using System.Text;
using WhatsOnTheFridge.Mobile.Core.Constants;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core.ViewModels
{
  public class MainMenuItemViewModel : BindableObject
  {
    private string _menuText;
    private MenuItemType _menuItemType;
    private Type _viewModelToLoad;

    public MenuItemType MenuItemType
    {
      get
      {
        return _menuItemType;
      }
      set
      {
        _menuItemType = value;
        OnPropertyChanged();
      }
    }

    public string MenuText
    {
      get
      {
        return _menuText;
      }
      set
      {
        _menuText = value;
        OnPropertyChanged();
      }
    }

    public Type ViewModelToLoad
    {
      get
      {
        return _viewModelToLoad;
      }
      set
      {
        _viewModelToLoad = value;
        OnPropertyChanged();
      }
    }
  }
}
