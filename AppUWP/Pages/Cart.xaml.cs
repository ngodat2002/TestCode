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
using AppUWP.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Cart : Page
    {
        public Cart()
        {
            this.InitializeComponent();
            //Adapters.SQLiteHelper sQLiteHelper = Adapters.SQLiteHelper.GetInstance();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RenderProductCart();
        }

        public void RenderProductCart()
        {
            int grandTotal = 0;

            Services.CartService cart = new Services.CartService();
            var ProductCart = cart.GetCarts();
            if (ProductCart != null)
            {
                CartProduct.ItemsSource = ProductCart;
                foreach (CartItem cartItem in ProductCart)
                {
                    grandTotal += cartItem.Total;
                }
                GrandTotal.Text = "Grand Total: " + grandTotal.ToString() + " VND";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Services.CartService cart = new Services.CartService();
            var cartItem = (sender as Button).DataContext as CartItem;
            cart.RemoveItem(cartItem);
            RenderProductCart();
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            Services.CartService cart = new Services.CartService();
            var cartItem = (sender as Button).DataContext as CartItem;
            if (cartItem.Qty <= 1)
            {
                return;
            }
            cart.UpdateItem(cartItem, cartItem.Qty - 1);
            RenderProductCart();
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            Services.CartService cart = new Services.CartService();
            var cartItem = (sender as Button).DataContext as CartItem;
            cart.UpdateItem(cartItem, cartItem.Qty + 1);
            RenderProductCart();
        }

        private async void CheckOut(object sender, RoutedEventArgs e)
        {
            Services.OrderService orderService = new Services.OrderService();
            Services.CartService cart = new Services.CartService();
            var cartItem = cart.GetCarts();
            var order = await orderService.CreateOrder(cartItem);
            CustomerModel customer = new CustomerModel()
            {
                Name = CusName.Text,
                Tel = CusTel.Text,
                Address = CusAddress.Text,
                OrderId = order.data.order_id,
                DateCheckOut = DateTime.Now
            };
            orderService.SaveCustomerOrderId(customer);

            CusName.Text = "";
            CusTel.Text = "";
            CusAddress.Text = "";
            cart.ClearCart();
            RenderProductCart();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainPage._frame.Navigate(typeof(Pages.Home));
        }
    }
}
