using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUWP.Models
{

    public class ItemOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        public int Total { get; set; }
    }

    public class DataOrder
    {
        public List<ItemOrder> items { get; set; }
        public int id { get; set; }
    }

    public class RootOrder
    {
        public string message { get; set; }
        public DataOrder data { get; set; }
    }

}
