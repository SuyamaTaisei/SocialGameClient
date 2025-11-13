using System;

[Serializable]
public class ShopCategoryModel
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
    public static void Insert(ShopCategoryModel[] shopCategoryModel)
    {
        foreach (ShopCategoryModel item in shopCategoryModel)
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
    public static ShopCategoryModel Select()
    {
        string query = "select * from shop_categories";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        ShopCategoryModel shopCategoryModel = new ShopCategoryModel();
        foreach (DataRow record in dataTable.Rows)
        {
            shopCategoryModel.category = int.Parse(record["category"].ToString());
            shopCategoryModel.name     = record["name"].ToString();
        }
        return shopCategoryModel;
    }
}