using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppUWP.Services
{
    class CategoryService
    {
        public async Task<Models.Categories> GetMenu()
        {
            Adapters.FoodGroup api = Adapters.FoodGroup.GetInstance();
            HttpClient hc = new HttpClient(); // shipper -> lo viec ket noi api de lay du lieu
            var rs = await hc.GetAsync(api.CategoryMenu); // get_file_content -> thao tacs tra hang cua shipper
            if (rs.StatusCode == HttpStatusCode.OK)
            {
                var stringContent = await rs.Content.ReadAsStringAsync(); // chuyen thanh string json
                Models.Categories categories = JsonConvert.DeserializeObject<Models.Categories>(stringContent); // Convert string json thanh 1 object DTO ( categories )
                return categories;
            }
            return null;
        }

        public async Task<Models.CategoryDetail> GetCategoryDetail(string id)
        {
            Adapters.FoodGroup api = Adapters.FoodGroup.GetInstance();
            HttpClient hc = new HttpClient();
            var rs = await hc.GetAsync(api.CategoryDetail(id));
            if (rs.StatusCode == HttpStatusCode.OK)
            {
                var stringContent = await rs.Content.ReadAsStringAsync(); // chuyen thanh string json
                Models.CategoryDetail categoryDetail = JsonConvert.DeserializeObject<Models.CategoryDetail>(stringContent); // Convert string json thanh 1 object DTO ( categories )
                return categoryDetail;
            }
            return null;
        }
        public async Task<Models.Products> GetFoodDetail(string id)
        {
            Adapters.FoodGroup api = Adapters.FoodGroup.GetInstance();
            HttpClient hc = new HttpClient();
            var rs = await hc.GetAsync(api.FoodDetail(id));
            if (rs.StatusCode == HttpStatusCode.OK)
            {
                var stringContent = await rs.Content.ReadAsStringAsync(); // chuyen thanh string json
                Models.Products products = JsonConvert.DeserializeObject<Models.Products>(stringContent); // Convert string json thanh 1 object DTO ( categories )
                return products;
            }
            return null;
        }
    }
}
