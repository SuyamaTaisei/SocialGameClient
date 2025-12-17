using System;

[Serializable]
public class CharacterCategoriesModel
{
    public int category;
    public string name;
}

public class CharacterCategoriesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists character_categories(" +
            "category int," +
            "name string," +
            "primary key(category))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(CharacterCategoriesModel[] characterCategoriesModel)
    {
        foreach (CharacterCategoriesModel item in characterCategoriesModel)
        {
            string query = "insert or replace into character_categories (" +
                "category," +
                "name" +
                ")" +
                "values (" + item.category + ", \"" + item.name + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
