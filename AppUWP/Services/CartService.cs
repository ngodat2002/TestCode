using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUWP.Models;
using SQLitePCL;
using AppUWP.Adapters;

namespace AppUWP.Services
{
    interface ICartService
    {
        List<CartItem> GetCarts();
        bool AddToCart(CartItem item);
        bool RemoveItem(CartItem item);
        bool UpdateItem(CartItem item, int qty);
        bool CheckCart(CartItem item);
        CartItem FindItem(CartItem item);
        bool ClearCart();
    }

    class CartService : ICartService
    {
        public bool AddToCart(CartItem item)
        {
            if (!CheckCart(item))
            {
                try
                {
                    SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                    string sql_txt = "insert into Cart(Id,Name,Image,Price,Qty) values(?,?,?,?,?)";
                    var statement = connection.Prepare(sql_txt);
                    statement.Bind(1, item.Id);
                    statement.Bind(2, item.Name);
                    statement.Bind(3, item.Image);
                    statement.Bind(4, item.Price);
                    statement.Bind(5, item.Qty);
                    var rs = statement.Step();
                    return rs == SQLiteResult.OK;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                CartItem cartItem = FindItem(item);
                try
                {
                    SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                    string sql_txt = "update Cart set Qty = ? where Id = ?";
                    var statement = connection.Prepare(sql_txt);
                    statement.Bind(1, item.Qty + cartItem.Qty);
                    statement.Bind(2, item.Id);
                    var rs = statement.Step();
                    return rs == SQLiteResult.OK;
                }
                catch
                {
                    return false;
                }
            }
            
        }

        public CartItem FindItem(CartItem item)
        {
            if (CheckCart(item))
            {
                var cartProduct = GetCarts();
                foreach (CartItem cartItem in cartProduct)
                {
                    if (cartItem.Id == item.Id)
                    {
                        return cartItem;
                    }
                }
            }
            return null;
        }

        public bool CheckCart(CartItem item)
        {
            var cartProduct = GetCarts();
            
            foreach (CartItem cartItem in cartProduct)
            {
                if(cartItem.Id == item.Id)
                {
                    return true;
                }
            }
            return false;

        }

        public bool ClearCart()
        {
            try
            {
                SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                string sql_txt = "delete from Cart";
                var statement = connection.Prepare(sql_txt);
                var rs = statement.Step();
                return rs == SQLiteResult.OK;
            }
            catch
            {
                return false;
            }
        }

        public List<CartItem> GetCarts()
        {
            List<CartItem> list = new List<CartItem>();

            try
            {
                SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                string sql_txt = "select * from Cart";
                var statement = connection.Prepare(sql_txt);
                while (SQLiteResult.ROW == statement.Step())
                {
                    CartItem item = new CartItem()
                    {
                        Id = Convert.ToInt32(statement[0]),
                        Name = statement[1] as string,
                        Image = statement[2] as string,
                        Price = Convert.ToInt32(statement[3]),
                        Qty = Convert.ToInt32(statement[4]),
                    };
                    list.Add(item);
                }
            }
            catch
            {

            }
            return list;
        }

        public bool RemoveItem(CartItem item)
        {
            try
            {
                SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                string sql_txt = "delete from Cart where Id = ?";
                var statement = connection.Prepare(sql_txt);
                statement.Bind(1, item.Id);
                var rs = statement.Step();
                return rs == SQLiteResult.OK;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateItem(CartItem item, int qty)
        {
            try
            {
                SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                string sql_txt = "update Cart set Qty = ? where Id = ?";
                var statement = connection.Prepare(sql_txt);
                statement.Bind(1, qty);
                statement.Bind(2, item.Id);
                var rs = statement.Step();
                return rs == SQLiteResult.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
