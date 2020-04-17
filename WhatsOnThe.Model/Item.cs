

using System;
using SQLite;

namespace WhatsOnThe.Model
{
  public class Item
  {
   // [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    //public DateTime? ExpirationDate { get; set; }
    //public DateTime AddedDate { get; set; }
    //public int Quantity { get; set; }
    //public int LocationId { get; set; }
    //public Location Location { get; set; }
  }
}
