using System;

[Serializable]
public class CharacterRaritiesModel
{
    public int id;
    public string name;
}

public class CharacterRaritiesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists character_rarities(" +
            "id int," +
            "name string," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(CharacterRaritiesModel[] characterRaritiesModel)
    {
        foreach (CharacterRaritiesModel item in characterRaritiesModel)
        {
            string query = "insert or replace into character_rarities (" +
                "id," +
                "name" +
                ")" +
                "values (" + item.id + ", \"" + item.name + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
