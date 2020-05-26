using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WhatsOnTheFridge.Mobile.Core.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOnTheFridge.Mobile.Core.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ItemsListView : PageBase
  {
    public ItemsListView()
    {
      InitializeComponent();
    }
  }
}
