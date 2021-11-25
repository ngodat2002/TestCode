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
    public sealed partial class Home : Page
    {

        public Home()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RenderProduct();
        }

        public async void RenderProduct()
        {
            Services.HomeSevice service = new Services.HomeSevice();
            var products = await service.getProduct();
            Services.FavoriteSevice favorite = new Services.FavoriteSevice();
            var favorites = favorite.GetFavoriteList();
            if (products != null)
            {
                foreach (Food food in products.data)
                {
                    food.icon = "\uEB51";
                }

                foreach (Food food in products.data)
                {
                    foreach (FavoriteItem Fitem in favorites)
                    {
                        if (food.id == Fitem.itemId)
                        {
                            food.icon = "\uEB52";
                        }
                    }
                }
                Gv.ItemsSource = products.data;

            }
        }

        private void GridViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Food food = Gv.SelectedItem as Food;
            Food item = (sender as GridViewItem).DataContext as Food;

            MainPage._frame.Navigate(typeof(Pages.ProductDetail), item);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Food item = (sender as Button).DataContext as Food;

            MainPage._frame.Navigate(typeof(Pages.ProductDetail), item);

        }

        private void AddFavorite_Click(object sender, RoutedEventArgs e)
        {
            Services.FavoriteSevice favorite = new Services.FavoriteSevice();
            Food item = (sender as Button).DataContext as Food;
            favorite.CheckFavoriteList(item);
            RenderProduct();
        }
    }
}
