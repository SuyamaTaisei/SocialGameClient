using System;

[Serializable]
public class GachaPeriodsModel
{
    public int id;
    public string name;
    public int single_cost;
    public int multi_cost;
    public string start;
    public string end;
}

public class GachaPeriodsTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists gacha_periods(" +
            "id int," +
            "name string," +
            "single_cost int," +
            "multi_cost int," +
            "start string," +
            "end string," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(GachaPeriodsModel[] gachaPeriodsModel)
    {
        foreach (GachaPeriodsModel item in gachaPeriodsModel)
        {
            string query = "insert or replace into gacha_periods (" +
                "id," +
                "name," +
                "single_cost," +
                "multi_cost," +
                "start," +
                "end" +
                ")" +
                "values (" + item.id + ", \"" + item.name + "\", " + item.single_cost + ", " + item.multi_cost + ", \"" + item.start + "\", \"" + item.end + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
