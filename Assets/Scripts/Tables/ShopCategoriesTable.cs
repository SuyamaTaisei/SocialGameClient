using System;

[Serializable]
public class ShopCategoriesModel
{
    public int category;
    public string name;
}

public static class ShopCategoriesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists shop_categories(" +
            "category int," +
            "name string," +
            "primary key(category))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(ShopCategoriesModel[] shopCategoriesModel)
    {
        foreach (ShopCategoriesModel item in shopCategoriesModel)
        {
            string query = "insert or replace into shop_categories (" +
                "category," +
                "name" +
                ")" +
                "values (" + item.category + ", \"" + item.name + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    //レコード取得
    public static ShopCategoriesModel Select()
    {
        string query = "select * from shop_categories";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        ShopCategoriesModel shopCategoriesModel = new ShopCategoriesModel();
        foreach (DataRow record in dataTable.Rows)
        {
            shopCategoriesModel.category = int.Parse(record["category"].ToString());
            shopCategoriesModel.name = record["name"].ToString();
        }
        return shopCategoriesModel;
    }
}