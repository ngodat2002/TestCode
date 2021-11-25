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
    public sealed partial class ListOrder : Page
    {
        public ListOrder()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RenderHistoryPaid();
        }

        public void RenderHistoryPaid()
        {
            Services.OrderService order = new Services.OrderService();
            var list = order.GetList();
            List.ItemsSource = list;
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CustomerModel order = List.SelectedItem as CustomerModel;
            MainPage._frame.Navigate(typeof(Pages.OrderProduct), order);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainPage._frame.Navigate(typeof(Pages.Home));
        }
    }
}
