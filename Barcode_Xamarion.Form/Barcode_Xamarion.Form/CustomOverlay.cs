using System.Linq;
using Xamarin.Forms;

namespace ZXing.Net.Mobile.Forms
{ 
    public class CustomOverlay : ZXingDefaultOverlay
    {
        public CustomOverlay()
        {
            foreach (var child in Children.OfType<BoxView>())
            {
                if (child == Children[2])
                {
                    child.BackgroundColor = Color.Transparent;
                    Margin = new Thickness(0, 0);
                }

                else
                {
                    child.Opacity = 0.5;
                }
            }
        }
    }
}