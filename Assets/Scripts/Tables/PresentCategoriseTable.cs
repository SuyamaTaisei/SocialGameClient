using System;

[Serializable]
public class PresentCategoriesModel
{
    public int category;
    public string name;
}

public class PresentCategoriseTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists present_categories(" +
            "category int," +
            "name string," +
            "primary key(category))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(PresentCategoriesModel[] presentCategoriesModel)
    {
        foreach (PresentCategoriesModel item in presentCategoriesModel)
        {
            string query = "insert or replace into present_categories (" +
                "category," +
                "name" +
                ")" +
                "values (" + item.category + ", \"" + item.name + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
