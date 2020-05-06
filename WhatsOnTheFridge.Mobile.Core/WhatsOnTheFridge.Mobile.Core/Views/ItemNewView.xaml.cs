using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOnTheFridge.Mobile.Core.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ItemNewView : ContentPage
  {
    public ItemNewView()
    {
      InitializeComponent();
      _suggestion = new List<string>() { "pera", "melon", "manzana", "mango", "maracuya" };
      LstSuggest.ItemsSource = _suggestion;
    }

    readonly List<string> _suggestion;

    // LstSuggest.ItemsSource = _suggestion;

    private void OnEntryChanged(object sender, TextChangedEventArgs e)
    {
      if (EntryName.Text != null && LstSuggest.ItemsSource != null)
      {
        if (_suggestion.Any(x => x.StartsWith(e.NewTextValue)) && EntryName.Text != string.Empty)
        {
          var items = new List<string>();

          foreach (var item in _suggestion.FindAll(x => x.StartsWith(e.NewTextValue)))
          {
            items.Add(item);
          }
          LstSuggest.IsVisible = true;
          LstSuggest.ItemsSource = items;
          LstSuggest.HeightRequest = items.Count * 23;
        }
        else
        {
          LstSuggest.IsVisible = false;
        }
      }
    }

    private void ItemSelected(object sender, ItemTappedEventArgs e)
    {
      if (((ListView)sender).SelectedItem == null)
        return;

      EntryName.Text = LstSuggest.SelectedItem.ToString();
      ((ListView)sender).SelectedItem = null;
      LstSuggest.IsVisible = false;
    }
  }
}