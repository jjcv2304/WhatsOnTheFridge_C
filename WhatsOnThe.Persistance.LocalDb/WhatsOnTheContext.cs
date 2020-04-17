using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WhatsOnThe.Model;
using Xamarin.Forms;

namespace WhatsOnThe.Persistance.LocalDb
{
  public class WhatsOnTheContext: DbContext
  {
    public DbSet<Location> Locations { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var dbPath = "SQLiteDataBase.db3";
      switch (Device.RuntimePlatform)
      {
        case Device.iOS:
        case Device.Android:
          dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbPath);
          break;
      }
      optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
  }
  
}
