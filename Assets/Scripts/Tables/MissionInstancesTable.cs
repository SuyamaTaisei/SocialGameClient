using System;
using System.Collections.Generic;

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

    //受取の有無 かつ 進捗が完了している全レコード取得
    public static List<MissionInstancesModel> SelectAll(int received, int limit)
    {
        string query = $"select mi.* from mission_instances as mi inner join mission_data as md on md.id = mi.mission_id where mi.received = {received} and mi.progress >= md.goal order by mi.id desc limit {limit}";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<MissionInstancesModel> result = new List<MissionInstancesModel>();

        foreach (DataRow record in dataTable.Rows)
        {
            MissionInstancesModel missionInstancesModel = new MissionInstancesModel();
            missionInstancesModel.id = int.Parse(record["id"].ToString());
            missionInstancesModel.manage_id = int.Parse(record["manage_id"].ToString());
            missionInstancesModel.mission_id = int.Parse(record["mission_id"].ToString());
            missionInstancesModel.mission_category = int.Parse(record["mission_category"].ToString());
            missionInstancesModel.progress = int.Parse(record["progress"].ToString());
            missionInstancesModel.cleared = int.Parse(record["cleared"].ToString());
            missionInstancesModel.received = int.Parse(record["received"].ToString());
            missionInstancesModel.created_at = record["created_at"].ToString();
            missionInstancesModel.updated_at = record["updated_at"].ToString();

            result.Add(missionInstancesModel);
        }

        return result;
    }

    //一致したミッションインスタンスだけを取得
    public static MissionInstancesModel SelectId(int missionId, int category)
    {
        string query = $"select * from mission_instances where mission_id = {missionId} and mission_category = {category}";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        MissionInstancesModel missionInstancesModel = null;

        foreach (DataRow record in dataTable.Rows)
        {
            missionInstancesModel = new MissionInstancesModel();
            missionInstancesModel.id = int.Parse(record["id"].ToString());
            missionInstancesModel.manage_id = int.Parse(record["manage_id"].ToString());
            missionInstancesModel.mission_id = int.Parse(record["mission_id"].ToString());
            missionInstancesModel.mission_category = int.Parse(record["mission_category"].ToString());
            missionInstancesModel.progress = int.Parse(record["progress"].ToString());
            missionInstancesModel.cleared = int.Parse(record["cleared"].ToString());
            missionInstancesModel.received = int.Parse(record["received"].ToString());
            missionInstancesModel.created_at = record["created_at"].ToString();
            missionInstancesModel.updated_at = record["updated_at"].ToString();
            break;
        }

        return missionInstancesModel;
    }

    //全件分一致したミッションインスタンスだけを取得
    public static List<MissionInstancesModel> SelectIdAll(int missionId, int category)
    {
        string query = $"select * from mission_instances where mission_id = {missionId} and mission_category = {category}";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<MissionInstancesModel> result = new List<MissionInstancesModel>();

        foreach (DataRow record in dataTable.Rows)
        {
            MissionInstancesModel missionInstancesModel = new MissionInstancesModel();
            missionInstancesModel.id = int.Parse(record["id"].ToString());
            missionInstancesModel.manage_id = int.Parse(record["manage_id"].ToString());
            missionInstancesModel.mission_id = int.Parse(record["mission_id"].ToString());
            missionInstancesModel.mission_category = int.Parse(record["mission_category"].ToString());
            missionInstancesModel.progress = int.Parse(record["progress"].ToString());
            missionInstancesModel.cleared = int.Parse(record["cleared"].ToString());
            missionInstancesModel.received = int.Parse(record["received"].ToString());
            missionInstancesModel.created_at = record["created_at"].ToString();
            missionInstancesModel.updated_at = record["updated_at"].ToString();

            result.Add(missionInstancesModel);
        }

        return result;
    }
}
