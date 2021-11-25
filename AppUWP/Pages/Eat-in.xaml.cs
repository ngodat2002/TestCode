using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using AppUWP.Models;
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
    public sealed partial class Eat_in : Page
    {
        public Eat_in()
        {
            this.InitializeComponent();
        }
        private Category _category;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Category category = e.Parameter as Category;
            _category = category;
            CategoryName.Text = category.name;
            RenderFoods(category);
        }

        public async void RenderFoods(Category category)
        {
            Services.CategoryService service = new Services.CategoryService();
            var CategoryDetail = await service.GetCategoryDetail(category.id.ToString());
            Services.FavoriteSevice favorite = new Services.FavoriteSevice();
            var favorites = favorite.GetFavoriteList();
            if (CategoryDetail != null)
            {
                foreach (Food food in CategoryDetail.data.foods)
                {
                    food.icon = "\uEB51";
                }

                foreach (Food food in CategoryDetail.data.foods)
                {
                    foreach (FavoriteItem Fitem in favorites)
                    {
                        if (food.id == Fitem.itemId)
                        {
                            food.icon = "\uEB52";
                        }
                    }
                }
                Gv.ItemsSource = CategoryDetail.data.foods;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainPage._frame.Navigate(typeof(Pages.Home));
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
            RenderFoods(_category);
        }
    }
}
