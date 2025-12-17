using System;
using System.Collections.Generic;

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

    //ガチャ期間IDの一致するレコード一覧を取得
    public static List<GachaDataModel> SelectAllGachaId(int gachaId)
    {
        string query = "select * from gacha_data where gacha_id = " + gachaId;
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<GachaDataModel> list = new();

        foreach (DataRow record in dataTable.Rows)
        {
            //必ずインスタンスを生成
            GachaDataModel gachaDataModel = new();
            gachaDataModel.gacha_id = int.Parse(record["gacha_id"].ToString());
            gachaDataModel.character_id = int.Parse(record["character_id"].ToString());
            gachaDataModel.weight = int.Parse(record["weight"].ToString());
            list.Add(gachaDataModel);
        }

        return list;
    }
}
