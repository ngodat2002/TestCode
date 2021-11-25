using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using AppUWP.Models;
using AppUWP.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductDetail : Page
    {
        public ProductDetail()
        {
            this.InitializeComponent();
        }

        private Food food;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            food = e.Parameter as Food;
            Price.Text = food.price.ToString() + " VND";
            Name.Text = food.name;
            //Description.Text = food.description;
            ImageProduct.UriSource = new Uri(food.image);
        }

        private void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            CartService cart = new CartService();
            CartItem item = new CartItem()
            {
                Id = food.id,
                Name = food.name,
                Image = food.image,
                Price = food.price,
                Qty = 1
            };
            cart.AddToCart(item);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainPage._frame.GoBack();
        }
    }
}
