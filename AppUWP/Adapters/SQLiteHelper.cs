using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using Windows.Storage;
using System.IO;

namespace AppUWP.Adapters
{
    class SQLiteHelper
    {
        private readonly string dbName = "AppUWP.db";

        private static SQLiteHelper sQLiteHelper;

        public SQLiteConnection _sQLiteConnection { get; set; }

        private SQLiteHelper()
        {
            // connect db
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            _sQLiteConnection = new SQLiteConnection(path);

            // tao bang Cart
            var sql_txt = @"create table if not exists Cart(Id integer primary key, Name varchar(255), Image varchar(255), Price integer, Qty integer)";
            var statement = _sQLiteConnection.Prepare(sql_txt);
            statement.Step();

            var sql_txt_order = @"create table if not exists Customer_Order(Id integer primary key, Name varchar(255), Tel char(20), Address text, OrderId integer,DateCheckOut datetime)";
            var statement_order = _sQLiteConnection.Prepare(sql_txt_order);
            statement_order.Step();
            var sql_favorite = @"create table if not exists Favorite(Id integer primary key,itemId integer)";
            var statement_favorite = _sQLiteConnection.Prepare(sql_favorite);
            statement_favorite.Step();
            
        }

        public static SQLiteHelper GetInstance()
        {
            if(sQLiteHelper == null)
            {
                sQLiteHelper = new SQLiteHelper();
            }
            return sQLiteHelper;
        }
    }
}
