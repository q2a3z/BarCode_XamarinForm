using Barcode_Xamarion.Form.ViewModels;
using Barcode_Xamarion.Form.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Barcode_Xamarion.Form
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
