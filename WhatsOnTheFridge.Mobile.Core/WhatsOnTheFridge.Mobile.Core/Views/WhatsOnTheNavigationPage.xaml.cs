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
  public partial class WhatsOnTheNavigationPage : NavigationPage
  {
    public WhatsOnTheNavigationPage()
    {
      InitializeComponent();
    }

    public WhatsOnTheNavigationPage(Page root): base(root)
    {
      InitializeComponent();
    }
  }
}