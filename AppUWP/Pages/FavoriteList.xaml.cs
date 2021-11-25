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
    public sealed partial class FavoriteList : Page
    {
        public FavoriteList()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RenderProduct();
        }

        public async void RenderProduct()
        {
            Services.FavoriteSevice favorite = new Services.FavoriteSevice();
            var products =  favorite.GetFavoriteList();
            if (products != null)
            {
                Gv.Items.Clear();
               foreach(FavoriteItem item in products)
                {
                    Services.CategoryService food = new Services.CategoryService();
                    var data =  await food.GetFoodDetail(item.itemId.ToString());
                    Gv.Items.Add(data.data);
                }

            }
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainPage._frame.Navigate(typeof(Pages.Home));

        }
    }
}
