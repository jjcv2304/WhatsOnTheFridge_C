using WhatsOnThe.Persistance.LocalDb;
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
          //database = new WhatsOnTheDatabase();
          database = new WhatsOnTheDatabase();
        }
        return database;
      }
    }

    public App()
    {
      InitializeComponent();

      MainPage = new MainPage();
      Database.Init();

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
