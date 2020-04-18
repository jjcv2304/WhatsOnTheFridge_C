using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using WhatsOnThe.Model;

namespace WhatsOnThe.Persistance.LocalDb
{
  public class WhatsOnTheDatabase
  {
    private static readonly Lazy<SQLiteAsyncConnection> LazyInitializer = 
      new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags));

    //public static SQLiteAsyncConnection Connection => LazyInitializer.Value;
    public SQLiteAsyncConnection Connection => LazyInitializer.Value;
    static bool _initialized = false;

    public WhatsOnTheDatabase()
    {
      InitializeAsync().SafeFireAndForget(false);
    }

    public void Init()
    {

    }

    async Task InitializeAsync()
    {
      if (!_initialized)
      {

//        await Database.ExecuteAsync(@"
//              create table Location(	
//                              Id integer not null primary key autoincrement,
//                              Name varchar not null,	
//                              Description varchar,
//                              Height int,
//                              Width int,
//                              Depth int
//                              );

//commit;
//              create table Item(	
//                              Id integer not null primary key autoincrement,
//                              Name varchar not null,	
//                              Description varchar,
//                              ExpirationDate integer,
//                              AddedDate integer not null default current_date,
//                              Quantity integer default 1 not null,
//                              LocationId integer references Location
//                              );
//commit;
//          ");

        // await Database.CreateTablesAsync(CreateFlags.None, typeof(Item)).ConfigureAwait(false);
        //await Database.CreateTablesAsync(CreateFlags.None, typeof(Location)).ConfigureAwait(false);


        Connection.GetConnection();

        _initialized = true;
      }
    }

   

  }
}
