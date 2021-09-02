using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Barcode_Xamarion.Form.ViewModels
{
    public class BarcodeScanModel : BaseViewModel
    {
        public BarcodeScanModel()
        {
            Title = "Code Scanner";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}