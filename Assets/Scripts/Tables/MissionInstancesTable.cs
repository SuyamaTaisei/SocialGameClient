using System;

[Serializable]
public class MissionInstancesModel
{
    public int id;
    public int manage_id;
    public int mission_id;
    public int mission_category;
    public int progress;
    public int cleared;
    public int received;
    public string created_at;
    public string updated_at;
}

public class MissionInstancesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists mission_instances(" +
            "id int," +
            "manage_id int," +
            "mission_id int," +
            "mission_category int," +
            "progress int," +
            "cleared int," +
            "received int," +
            "created_at string," +
            "updated_at string," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(MissionInstancesModel[] missionInstancesModel)
    {
        foreach (MissionInstancesModel item in missionInstancesModel)
        {
            string query = "insert or replace into mission_instances (" +
                "id," +
                "manage_id," +
                "mission_id," +
                "mission_category," +
                "progress," +
                "cleared," +
                "received," +
                "created_at," +
                "updated_at" +
                ")" +
                "values (" + item.id + ", " + item.manage_id + ", " + item.mission_id + ", " + item.mission_category + ", " + item.progress + ", " + item.cleared + ", " + item.received + ", \"" + item.created_at + "\", \"" + item.updated_at + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
