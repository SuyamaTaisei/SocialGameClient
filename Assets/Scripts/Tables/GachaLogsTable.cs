using System;

[Serializable]
public class GachaLogsModel
{
    public int id;
    public int manage_id;
    public int gacha_id;
    public int character_id;
    public string created_at;
}

public class GachaLogsTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists gacha_logs(" +
            "id int," +
            "manage_id int," +
            "gacha_id int," +
            "character_id int," +
            "created_at string," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(GachaLogsModel[] gachaLogsModel)
    {
        foreach (GachaLogsModel item in gachaLogsModel)
        {
            string query = "insert or replace into gacha_logs (" +
                "id," +
                "manage_id," +
                "gacha_id," +
                "character_id," +
                "created_at" +
                ")" +
                "values (" + item.id + ", " + item.manage_id + ", " + item.gacha_id + ", " + item.character_id + ", \"" + item.created_at + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
