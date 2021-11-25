using AppUWP.Models;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderProduct : Page
    {
        public OrderProduct()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CustomerModel orderItem = e.Parameter as CustomerModel;
            RenderOrder(orderItem);
        }

        public async void RenderOrder(CustomerModel order)
        {
            var grandTotal = 0;
            Services.OrderDetailService service = new Services.OrderDetailService();
            var OrderProduct = await service.GetOrderDetail(order.OrderId.ToString());
            if(OrderProduct != null)
            {
                ListOrderProduct.ItemsSource = OrderProduct.data.items;
                foreach (ItemOrder cartItem in OrderProduct.data.items)
                {
                    grandTotal += cartItem.Total;
                }
                GrandTotal.Text = "Grand Total: " + grandTotal.ToString() + " VND";
            }

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainPage._frame.Navigate(typeof(Pages.ListOrder));
        }
    }
}
