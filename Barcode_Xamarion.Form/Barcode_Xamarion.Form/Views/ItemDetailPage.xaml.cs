using Barcode_Xamarion.Form.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Barcode_Xamarion.Form.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
            var barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 300;
            barcode.BarcodeOptions.Height = 300;
            barcode.BarcodeOptions.Margin = 10;
            barcode.BarcodeValue = "ZXing.Net.Mobile";

            stacklayout.Children.Add(barcode);
        }
    }
}