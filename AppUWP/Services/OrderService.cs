using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using Windows.Storage.Streams;
using SQLitePCL;
using AppUWP.Adapters;

namespace AppUWP.Services
{
    class OrderService
    {
        public async Task<Models.CreateOrder> CreateOrder(List<Models.CartItem> items)
        {
            // cach 2: items co the tao 1 bien de lay du lieu san pham co trong gio hang
            /*CartService cs = new CartService();
            var items = cs.GetCarts();*/

            Adapters.FoodGroup fg = Adapters.FoodGroup.GetInstance();
            HttpClient httpClient = new HttpClient();
            Uri uri = new Uri(fg.ApiCreateOrder);
            HttpStringContent content = new HttpStringContent(
                    "{ \"items\" :" +
                    JsonConvert.SerializeObject(items)+"}",
                    UnicodeEncoding.Utf8,
                    "application/json"
                );
            HttpResponseMessage msg = await httpClient.PostAsync(uri, content);
            msg.EnsureSuccessStatusCode();
            var rsBody = await msg.Content.ReadAsStringAsync();
            Models.CreateOrder createOrder = JsonConvert.DeserializeObject<Models.CreateOrder>(rsBody);
            return createOrder;

            // Sau khi nhan duoc order_id -> luu vao 1 table trong SQLite de lam trang danh sach don hang
        }

        

        public bool SaveCustomerOrderId(Models.CustomerModel customer)
        {
            try
            {
                SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                string sql_txt = "insert into Customer_Order(Name,Tel,Address,OrderId,DateCheckOut) values(?,?,?,?,?)";
                var statement = connection.Prepare(sql_txt);
                statement.Bind(1, customer.Name);
                statement.Bind(2, customer.Tel);
                statement.Bind(3, customer.Address);
                statement.Bind(4, customer.OrderId);
                statement.Bind(5, customer.DateCheckOut.ToString());
                var rs = statement.Step();
                return rs == SQLiteResult.OK;
            }
            catch
            {
                return false;
            }
        }

        public List<Models.CustomerModel> GetList()
        {
            List<Models.CustomerModel> list = new List<Models.CustomerModel>();

            try
            {
                SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                string sql_txt = "select * from Customer_Order";
                var statement = connection.Prepare(sql_txt);
                while (SQLiteResult.ROW == statement.Step())
                {
                    Models.CustomerModel item = new Models.CustomerModel()
                    {
                        Id = Convert.ToInt32(statement[0]),
                        Name = statement[1] as string,
                        Tel = statement[2] as string,
                        Address = statement[3] as string,
                        OrderId = Convert.ToInt32(statement[4]),
                        DateCheckOut =Convert.ToDateTime( statement[5]),
                    };
                    list.Add(item);
                }
            }
            catch
            {

            }
            return list;
        }
    }
}
