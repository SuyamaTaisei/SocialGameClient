using System;
using System.Collections.Generic;

[Serializable]
public class ShopDataModel
{
    public int product_id;
    public int shop_category;
    public string type;
    public string name;
    public int paid_currency;
    public int free_currency;
    public int coin_currency;
    public int price;
}

public static class ShopDataTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists shop_data(" +
            "product_id int," +
            "shop_category int," +
            "type string," +
            "name string," +
            "paid_currency int," +
            "free_currency int," +
            "coin_currency int," +
            "price int," +
            "primary key(product_id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(ShopDataModel[] shopDataModel)
    {
        foreach (ShopDataModel item in shopDataModel)
        {
            string query = "insert or replace into shop_data (" +
                "product_id," +
                "shop_category," +
                "type," +
                "name," +
                "paid_currency," +
                "free_currency," +
                "coin_currency," +
                "price" +
                ")" +
                "values (" + item.product_id + ", " + item.shop_category + ", \"" + item.type + "\", \"" + item.name + "\", " + item.paid_currency + ", " + item.free_currency + ", " + item.coin_currency + ", " + item.price + ")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    //レコード1件取得
    public static ShopDataModel Select()
    {
        string query = "select * from shop_data";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        ShopDataModel shopDataModel = new ShopDataModel();

        foreach (DataRow record in dataTable.Rows)
        {
            shopDataModel.product_id = int.Parse(record["product_id"].ToString());
            shopDataModel.shop_category = int.Parse(record["shop_category"].ToString());
            shopDataModel.type = record["type"].ToString();
            shopDataModel.name = record["name"].ToString();
            shopDataModel.paid_currency = int.Parse(record["paid_currency"].ToString());
            shopDataModel.free_currency = int.Parse(record["free_currency"].ToString());
            shopDataModel.coin_currency = int.Parse(record["coin_currency"].ToString());
            shopDataModel.price = int.Parse(record["price"].ToString());
        }

        return shopDataModel;
    }

    //全レコード取得
    public static List<ShopDataModel> SelectAll()
    {
        string query = "select * from shop_data";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<ShopDataModel> result = new List<ShopDataModel>();

        foreach (DataRow record in dataTable.Rows)
        {
            ShopDataModel shopDataModel = new ShopDataModel();
            shopDataModel.product_id = int.Parse(record["product_id"].ToString());
            shopDataModel.shop_category = int.Parse(record["shop_category"].ToString());
            shopDataModel.type = record["type"].ToString();
            shopDataModel.name = record["name"].ToString();
            shopDataModel.paid_currency = int.Parse(record["paid_currency"].ToString());
            shopDataModel.free_currency = int.Parse(record["free_currency"].ToString());
            shopDataModel.coin_currency = int.Parse(record["coin_currency"].ToString());
            shopDataModel.price = int.Parse(record["price"].ToString());

            result.Add(shopDataModel);
        }

        return result;
    }

    //商品IDの一致するレコードを取得
    public static ShopDataModel SelectProductId(int productId)
    {
        string query = "select * from shop_data where product_id = " + productId;
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        ShopDataModel shopDataModel = null;

        foreach (DataRow record in dataTable.Rows)
        {
            //必ずインスタンスを生成
            shopDataModel = new ShopDataModel();
            shopDataModel.product_id = int.Parse(record["product_id"].ToString());
            shopDataModel.shop_category = int.Parse(record["shop_category"].ToString());
            shopDataModel.type = record["type"].ToString();
            shopDataModel.name = record["name"].ToString();
            shopDataModel.paid_currency = int.Parse(record["paid_currency"].ToString());
            shopDataModel.free_currency = int.Parse(record["free_currency"].ToString());
            shopDataModel.coin_currency = int.Parse(record["coin_currency"].ToString());
            shopDataModel.price = int.Parse(record["price"].ToString());
            break;
        }

        return shopDataModel;
    }
}