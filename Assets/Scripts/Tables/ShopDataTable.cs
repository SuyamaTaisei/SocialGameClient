using System;

[Serializable]
public class ShopDataModel
{
    public int product_id;
    public int shop_category;
    public string name;
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
            "name string," +
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
                "name," +
                "price" +
                ")" +
                "values (" + item.product_id + ", " + item.shop_category + ", \"" + item.name + "\", " + item.price + ")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    //レコード取得
    public static ShopDataModel Select()
    {
        string query = "select * from shop_data";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        ShopDataModel shopDataModel = new ShopDataModel();
        foreach (DataRow record in dataTable.Rows)
        {
            shopDataModel.product_id    = int.Parse(record["product_id"].ToString());
            shopDataModel.shop_category = int.Parse(record["shop_category"].ToString());
            shopDataModel.name          = record["name"].ToString();
            shopDataModel.price         = int.Parse(record["price"].ToString());
        }
        return shopDataModel;
    }
}