using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOnThe.Model;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core
{
  // Learn more about making custom code visible in the Xamarin.Forms previewer
  // by visiting https://aka.ms/xamarinforms-previewer
  [DesignTimeVisible(false)]
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
    }

    private  void OnClick_ItemAdd(object sender, EventArgs e)
    {
      var todoItem = new Item()
      {
        Name = "Test item " + DateTime.Now.TimeOfDay.ToString(),
        Description = "Desc"
      };
       App.Database.SaveItemAsync(todoItem);

    }
    private  void OnClick_LocationAdd(object sender, EventArgs e)
    {
      var todoItem = new Location()
      {
        Name = "Test loc " + DateTime.Now.TimeOfDay.ToString(),
        Description = "Desc loc",
      };
       App.Database.SaveLocationAsync(todoItem);

    }
    private  void OnClick_GetItems(object sender, EventArgs e)
    {
      var items =  App.Database.GetItemsAsync();
    }
    private  void OnClick_GetLocations(object sender, EventArgs e)
    {
      var items =  App.Database.GetLocationsAsync();
    }
  }
}
