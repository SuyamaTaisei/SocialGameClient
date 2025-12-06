using System;

[Serializable]
public class GachaPeriodsModel
{
    public int id;
    public string name;
    public int single_count;
    public int multi_count;
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
            "single_count int," +
            "multi_count int," +
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
                "single_count," +
                "multi_count," +
                "single_cost," +
                "multi_cost," +
                "start," +
                "end" +
                ")" +
                "values (" + item.id + ", \"" + item.name + "\", " + item.single_count + ", " + item.multi_count + ", " + item.single_cost + ", " + item.multi_cost + ", \"" + item.start + "\", \"" + item.end + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    //ガチャ期間IDの一致するレコードを取得
    public static GachaPeriodsModel SelectId(int id)
    {
        string query = "select * from gacha_periods where id = " + id;
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        GachaPeriodsModel gachaPeriodsModel = null;

        foreach (DataRow record in dataTable.Rows)
        {
            //必ずインスタンスを生成
            gachaPeriodsModel = new GachaPeriodsModel();
            gachaPeriodsModel.id = int.Parse(record["id"].ToString());
            gachaPeriodsModel.name = record["name"].ToString();
            gachaPeriodsModel.single_count = int.Parse(record["single_count"].ToString());
            gachaPeriodsModel.multi_count = int.Parse(record["multi_count"].ToString());
            gachaPeriodsModel.single_cost = int.Parse(record["single_cost"].ToString());
            gachaPeriodsModel.multi_cost = int.Parse(record["multi_cost"].ToString());
            gachaPeriodsModel.start = record["start"].ToString();
            gachaPeriodsModel.end = record["end"].ToString();
            break;
        }

        return gachaPeriodsModel;
    }
}
