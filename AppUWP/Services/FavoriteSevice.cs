using AppUWP.Adapters;
using AppUWP.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUWP.Services
{
    interface IFavoriteSevice
    {
        List<FavoriteItem> GetFavoriteList();
        bool AddFavoriteList(Food item);
        bool RemoveItem(Food item);
        bool CheckFavoriteList(Food item);
        bool ClearFavoriteList();
    }
    class FavoriteSevice : IFavoriteSevice
    {
        public bool AddFavoriteList(Food item)
        {
            try
            {
                SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                string sql_txt = "insert into Favorite(itemId) values(?)";
                var statement = connection.Prepare(sql_txt);
                statement.Bind(1, item.id);
                var rs = statement.Step();
                return rs == SQLiteResult.OK;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckFavoriteList(Food item)
        {
            var cartProduct = GetFavoriteList();

            foreach (FavoriteItem cartItem in cartProduct)
            {
                if (cartItem.itemId == item.id)
                {
                    RemoveItem(item);
                    return true;
                }
            }
            AddFavoriteList(item);
            return false;

        }

        public bool ClearFavoriteList()
        {
            throw new NotImplementedException();
        }

        public List<FavoriteItem> GetFavoriteList()
        {
            List<FavoriteItem> list = new List<FavoriteItem>();

            try
            {
                SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                string sql_txt = "select * from Favorite";
                var statement = connection.Prepare(sql_txt);
                while (SQLiteResult.ROW == statement.Step())
                {
                    FavoriteItem item = new FavoriteItem()
                    {
                        Id = Convert.ToInt32(statement[0]),                                           
                        itemId = Convert.ToInt32(statement[1]),
                    };
                    list.Add(item);
                }
            }
            catch
            {

            }
            return list;
        }
        public bool RemoveItem(Food item)
        {
            try
            {
                SQLiteConnection connection = SQLiteHelper.GetInstance()._sQLiteConnection;
                string sql_txt = "delete from Favorite where itemId = ?";
                var statement = connection.Prepare(sql_txt);
                statement.Bind(1, item.id);
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
