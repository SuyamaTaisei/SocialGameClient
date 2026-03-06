using System;
using System.Collections.Generic;

[Serializable]
public class MissionDataModel
{
    public int id;
    public int mission_category;
    public int goal;
    public string description;
    public int reward_category;
    public string reward_value;
}

public class MissionDataTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists mission_data(" +
            "id int," +
            "mission_category int," +
            "goal int," +
            "description string," +
            "reward_category int," +
            "reward_value string," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(MissionDataModel[] missionDataModel)
    {
        foreach (MissionDataModel item in missionDataModel)
        {
            string query = "insert or replace into mission_data (" +
                "id," +
                "mission_category," +
                "goal," +
                "description," +
                "reward_category," +
                "reward_value" +
                ")" +
                "values (@id, @mission_category, @goal, @description, @reward_category, @reward_value)";
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"@id", item.id },
                {"@mission_category", item.mission_category },
                {"@goal", item.goal },
                {"@description", item.description },
                {"@reward_category", item.reward_category },
                {"@reward_value", item.reward_value },
            };
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query, param);
        }
    }

    //受取済みの有無で全レコード取得
    public static List<MissionDataModel> SelectAll(int limit)
    {
        string query = $"select * from mission_data order by id desc limit {limit}";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<MissionDataModel> result = new List<MissionDataModel>();

        foreach (DataRow record in dataTable.Rows)
        {
            MissionDataModel missionDataModel = new MissionDataModel();
            missionDataModel.id = int.Parse(record["id"].ToString());
            missionDataModel.mission_category = int.Parse(record["mission_category"].ToString());
            missionDataModel.goal = int.Parse(record["goal"].ToString());
            missionDataModel.description = record["description"].ToString();
            missionDataModel.reward_category = int.Parse(record["reward_category"].ToString());
            missionDataModel.reward_value = record["reward_value"].ToString();

            result.Add(missionDataModel);
        }

        return result;
    }

    //一致したミッションデータだけを取得
    public static MissionDataModel SelectId(int id)
    {
        string query = "select * from mission_data where id = " + id;
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        MissionDataModel missionDataModel = null;

        foreach (DataRow record in dataTable.Rows)
        {
            missionDataModel = new MissionDataModel();
            missionDataModel.id = int.Parse(record["id"].ToString());
            missionDataModel.mission_category = int.Parse(record["mission_category"].ToString());
            missionDataModel.goal = int.Parse(record["goal"].ToString());
            missionDataModel.description = record["description"].ToString();
            missionDataModel.reward_category = int.Parse(record["reward_category"].ToString());
            missionDataModel.reward_value = record["reward_value"].ToString();
            break;
        }

        return missionDataModel;
    }
}
