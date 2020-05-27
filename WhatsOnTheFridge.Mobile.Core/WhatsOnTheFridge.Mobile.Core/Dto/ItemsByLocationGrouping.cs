using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WhatsOnThe.Model;

namespace WhatsOnTheFridge.Mobile.Core.Dto
{
  public class ItemsByLocationGrouping: List<Item>
  {
    public int Id { get; set; }
    public string Location { get; set; }

    public ItemsByLocationGrouping(int id, string location)
    {
      Id = id;
      Location = location;
    }
  }
}
