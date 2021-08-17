using Barcode_Xamarion.Form.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Barcode_Xamarion.Form.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}