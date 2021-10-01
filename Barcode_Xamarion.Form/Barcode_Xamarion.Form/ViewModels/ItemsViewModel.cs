using Barcode_Xamarion.Form.Models;
using Barcode_Xamarion.Form.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barcode_Xamarion.Form.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            OnLoad();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
        private async void OnLoad()
        {
            #region How to load an Json file embedded resource
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Barcode_Xamarion.Form.TestBarcode.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var rootobject = JsonConvert.DeserializeObject<List<Item>>(json);
                foreach (Item codeObject in rootobject) 
                {
                    codeObject.Id = Guid.NewGuid().ToString();
                    await DataStore.AddItemAsync(codeObject);
                }
            }
            #endregion

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }


    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension ,IValueConverter
    {

        public string Source { get; set; }

        //Source→View
        //ViewModelやコードビハインドからXaml側へ
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string embeddedResourcePath = "Barcode_Xamarion.Form.Images.";
            if (value.ToString().Equals("QR_CODE"))
                embeddedResourcePath += "QRcode.png";
            else
                embeddedResourcePath += "barcode.png";
            var imageSource = ImageSource.FromResource(embeddedResourcePath, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return imageSource;
        }

        //View→Source
        //Xaml側からViewModelやコードビハインドへ
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {

            /*
            Source = "Barcode_Xamarion.Form.Images.barcode.png";

            if (Source == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return imageSource;
            */
            return this;
        }
    }
}