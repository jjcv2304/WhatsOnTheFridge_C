﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsOnTheFridge.Mobile.Core.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsOnTheFridge.Mobile.Core.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ItemDetailView : PageBase
  {
    public ItemDetailView()
    {
      InitializeComponent();
    }
  }
}