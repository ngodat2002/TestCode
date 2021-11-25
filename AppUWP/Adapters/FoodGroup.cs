using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUWP.Adapters
{
    class FoodGroup
    {
        private readonly string baseUrl = "http://foodgroup.herokuapp.com";

        private static FoodGroup instance;

        private FoodGroup()
        {

        }

        public static FoodGroup GetInstance()
        {
            if (instance == null)
            {
                instance = new FoodGroup();
            }
            return instance;
        }

        public string ApiHome
        {
            get => string.Format(baseUrl + "/api/today-special");
        }

        public string CategoryMenu
        {
            get => string.Format(baseUrl + "/api/menu");
        }

        public string CategoryDetail(string id)
        {
            return string.Format(baseUrl + "/api/category/" + id);
        }
        public string FoodDetail(string id)
        {
            return string.Format(baseUrl + "/api/food/" + id);
        }

        public string ApiCreateOrder
        {
            get => string.Format(baseUrl + "/api/create-order");
        }

        public string GetOrder(string id)
        {
            return string.Format(baseUrl + "/api/order/" + id);
        }
    }
}
