using System;
using System.Collections.Generic;

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

    //全レコード取得
    public static List<GachaLogsModel> SelectAll()
    {
        string query = "select * from gacha_logs";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<GachaLogsModel> result = new List<GachaLogsModel>();

        foreach (DataRow record in dataTable.Rows)
        {
            GachaLogsModel gachaLogsModel = new GachaLogsModel();
            gachaLogsModel.id = int.Parse(record["id"].ToString());
            gachaLogsModel.manage_id = int.Parse(record["manage_id"].ToString());
            gachaLogsModel.gacha_id = int.Parse(record["gacha_id"].ToString());
            gachaLogsModel.character_id = int.Parse(record["character_id"].ToString());
            gachaLogsModel.created_at = record["created_at"].ToString();

            result.Add(gachaLogsModel);
        }

        return result;
    }

    //任意の件数分、新しい順にガチャ履歴レコードを取得
    public static List<GachaLogsModel> SelectIdLatest(int limit)
    {
        string query = $"select * from gacha_logs order by id desc limit {limit}";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<GachaLogsModel> result = new List<GachaLogsModel>();

        foreach (DataRow record in dataTable.Rows)
        {
            GachaLogsModel gachaLogsModel = new GachaLogsModel();
            gachaLogsModel.id = int.Parse(record["id"].ToString());
            gachaLogsModel.manage_id = int.Parse(record["manage_id"].ToString());
            gachaLogsModel.gacha_id = int.Parse(record["gacha_id"].ToString());
            gachaLogsModel.character_id = int.Parse(record["character_id"].ToString());
            gachaLogsModel.created_at = record["created_at"].ToString();

            result.Add(gachaLogsModel);
        }

        return result;
    }
}
