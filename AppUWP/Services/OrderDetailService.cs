using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using AppUWP.Adapters;
using Newtonsoft.Json;

namespace AppUWP.Services
{
    class OrderDetailService
    {
        public async Task<Models.RootOrder> GetOrderDetail(string id)
        {
            FoodGroup api = FoodGroup.GetInstance();
            HttpClient hc = new HttpClient();
            var rs = await hc.GetAsync(api.GetOrder(id));
            if(rs.StatusCode == HttpStatusCode.OK)
            {
                var stringContent = await rs.Content.ReadAsStringAsync(); // chuyen thanh string json
                Models.RootOrder orderDetail = JsonConvert.DeserializeObject<Models.RootOrder>(stringContent); // Convert string json thanh 1 object DTO ( categories )
                return orderDetail;
            }
            return null;
        }
    }
}
