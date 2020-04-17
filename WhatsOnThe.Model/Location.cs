using System;
using System.Collections.Generic;
using System.Text;


namespace WhatsOnThe.Model
{
  public class Location
  {
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Depth { get; set; }
    //public int ParentLocationId { get; set; }
    //public Location ParentLocation { get; set; }
    //public ICollection<Location> ChildLocations { get; set; }
    //public ICollection<Item> Items { get; set; }
  }
}
