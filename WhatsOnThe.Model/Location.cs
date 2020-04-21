using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace WhatsOnThe.Model
{
  public class Location
  {
    
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Depth { get; set; }
    [OneToMany]  
    public List<Item> Items { get; set; }

    // TODO add levels of locations
    //[ForeignKey(typeof(Location))]	
    //public int ParentLocationId { get; set; }
    //[ManyToOne()]
    //public Location ParentLocation { get; set; }
    //[OneToMany()]
    //public ICollection<Location> ChildLocations { get; set; }
  }
}
