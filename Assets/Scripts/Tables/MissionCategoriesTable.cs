using System;

[Serializable]
public class MissionCategoriesModel
{
    public int category;
    public string name;
}

public class MissionCategoriesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists mission_categories(" +
            "category int," +
            "name string," +
            "primary key(category))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(MissionCategoriesModel[] missionCategoriesModel)
    {
        foreach (MissionCategoriesModel item in missionCategoriesModel)
        {
            string query = "insert or replace into mission_categories (" +
                "category," +
                "name" +
                ")" +
                "values (" + item.category + ", \"" + item.name + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
