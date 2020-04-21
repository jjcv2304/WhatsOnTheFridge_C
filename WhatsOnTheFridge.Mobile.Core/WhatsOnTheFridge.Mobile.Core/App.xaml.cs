using System.Threading.Tasks;
using WhatsOnThe.Persistance.LocalDb;
using WhatsOnTheFridge.Mobile.Core.Bootstrap;
using WhatsOnTheFridge.Mobile.Core.Contracts.Services.General;
using Xamarin.Forms;

namespace WhatsOnTheFridge.Mobile.Core
{
  public partial class App : Application
  {
    static WhatsOnTheDatabase database;
    public static WhatsOnTheDatabase Database
    {
      get
      {
        if (database == null)
        {
          database = new WhatsOnTheDatabase();
        }
        return database;
      }
    }

    public App()
    {
      InitializeComponent();

      InitializeApp();

      InitializeNavigation();

    //  Database.Init();

    }

    private async Task InitializeNavigation()
    {
      var navigationService = TinyContainer.Resolve<INavigationService>();
      await navigationService.InitializeAsync();
    }

    private void InitializeApp()
    {
      TinyContainer.RegisterDependencies();

      //var shoppingCartViewModel = TinyContainer.Resolve<ShoppingCartViewModel>();
      //shoppingCartViewModel.InitializeMessenger();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }



  }
}
