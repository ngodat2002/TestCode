using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace AppUWP.Services
{
    class HomeSevice
    {
        public async Task<Models.Foods> getProduct()
        {
            Adapters.FoodGroup api = Adapters.FoodGroup.GetInstance();
            HttpClient hc = new HttpClient();
            var rs = await hc.GetAsync(api.ApiHome);
            if(rs.StatusCode == HttpStatusCode.OK)
            {
                var stringContent = await rs.Content.ReadAsStringAsync();
                Models.Foods products = JsonConvert.DeserializeObject<Models.Foods>(stringContent);
                return products;
            }
            return null;
        }
    }
}
