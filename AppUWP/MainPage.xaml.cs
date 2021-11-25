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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Frame _frame;

        public MainPage()
        {
            this.InitializeComponent();
            _frame = MainFrame;
            MainFrame.Navigate(typeof(Pages.Home));
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            var item2 = new MenuModel() { Name = "Cart", NamePage = "cart", Icon = "/Assets/icon-cart-menu.png" };
            var item1 = new MenuModel() { Name = "Order", NamePage = "order", Icon = "/Assets/icon-bill-menu.png" };
            var item3 = new MenuModel() { Name = "Favorite", NamePage = "favorite", Icon = "/Assets/icon-heart.png" };
            var item4 = new MenuModel() { Name = "Delivery", NamePage = "delivery", Icon = "/Assets/button4.png" };
            var item5 = new MenuModel() { Name = "Take Away", NamePage = "take-away", Icon = "/Assets/button5.png" };
            var item6 = new MenuModel() { Name = "Driver Payment", NamePage = "driver-payment", Icon = "/Assets/button6.png" };
            var item7 = new MenuModel() { Name = "Customers", NamePage = "customers", Icon = "/Assets/button7.png" };

            Menu.Items.Add(item2);
            Menu.Items.Add(item1);
            Menu.Items.Add(item3);
            Menu.Items.Add(item4);
            Menu.Items.Add(item5);
            Menu.Items.Add(item6);
            Menu.Items.Add(item7);

            RenderCategoryToMenu();
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MenuModel selectedItem = (MenuModel)Menu.SelectedItem;
            switch (selectedItem.NamePage)
            {
                case "home": MainFrame.Navigate(typeof(Pages.Home), selectedItem); break;
                case "cart": MainFrame.Navigate(typeof(Pages.Cart), selectedItem); break;
                case "order": MainFrame.Navigate(typeof(Pages.ListOrder), selectedItem); break;
                case "favorite": MainFrame.Navigate(typeof(Pages.FavoriteList)); break;
            }
        }

        public async void RenderCategoryToMenu()
        {
            Services.CategoryService service = new Services.CategoryService();
            var CategoryMenu = await service.GetMenu();
            if (CategoryMenu != null)
            {
                MenuFlyout.ItemsSource = CategoryMenu.data;
            }
        }

        private void GridViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Category selectedItem = (Category)MenuFlyout.SelectedItem;
            switch (selectedItem.namePage)
            {
                case "category": MainFrame.Navigate(typeof(Pages.Eat_in), selectedItem); break;
            }
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(Pages.Home));
        }
    }
}
