using System;

[Serializable]
public class GachaDataModel
{
    public int gacha_id;
    public int character_id;
    public int weight;
}

public class GachaDataTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists gacha_data(" +
            "gacha_id int," +
            "character_id int," +
            "weight int," +
            "primary key(character_id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(GachaDataModel[] gachaDataModel)
    {
        foreach (GachaDataModel item in gachaDataModel)
        {
            string query = "insert or replace into gacha_data (" +
                "gacha_id," +
                "character_id," +
                "weight" +
                ")" +
                "values (" + item.gacha_id + ", " + item.character_id + ", " + item.weight + ")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
