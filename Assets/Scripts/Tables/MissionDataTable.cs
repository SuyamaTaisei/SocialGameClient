using System;

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
                "values (" + item.id + ", " + item.mission_category + ", " + item.goal + ", \"" + item.description + "\", " + item.reward_category + ", \"" + item.reward_value + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
