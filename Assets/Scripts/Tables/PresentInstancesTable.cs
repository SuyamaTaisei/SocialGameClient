using System;
using System.Collections.Generic;

[Serializable]
public class PresentInstancesModel
{
    public int id;
    public int manage_id;
    public int present_category;
    public string present_name;
    public int content;
    public int amount;
    public int received;
    public string period;
    public string created_at;
    public string updated_at;
}

public class PresentInstancesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists present_instances(" +
            "id int," +
            "manage_id int," +
            "present_category int," +
            "present_name string," +
            "content int," +
            "amount int," +
            "received bool," +
            "period string," +
            "created_at string," +
            "updated_at string," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(PresentInstancesModel[] presentInstancesModel)
    {
        foreach (PresentInstancesModel item in presentInstancesModel)
        {
            string query = "insert or replace into present_instances (" +
                "id," +
                "manage_id," +
                "present_category," +
                "present_name," +
                "content," +
                "amount," +
                "received," +
                "period," +
                "created_at," +
                "updated_at" +
                ")" +
                "values (" + item.id + ", " + item.manage_id + ", " + item.present_category + ", \"" + item.present_name + "\", " + item.content + ", " + item.amount + ", " + item.received + ", \"" + item.period + "\", \"" + item.created_at + "\", \"" + item.updated_at + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    //受取済みの有無で全レコード取得
    public static List<PresentInstancesModel> SelectAll(int received)
    {
        string query = "select * from present_instances where received = " + received;
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<PresentInstancesModel> result = new List<PresentInstancesModel>();

        foreach (DataRow record in dataTable.Rows)
        {
            PresentInstancesModel presentInstancesModel = new PresentInstancesModel();
            presentInstancesModel.id = int.Parse(record["id"].ToString());
            presentInstancesModel.manage_id = int.Parse(record["manage_id"].ToString());
            presentInstancesModel.present_category = int.Parse(record["present_category"].ToString());
            presentInstancesModel.present_name = record["present_name"].ToString();
            presentInstancesModel.content = int.Parse(record["content"].ToString());
            presentInstancesModel.amount = int.Parse(record["amount"].ToString());
            presentInstancesModel.received = int.Parse(record["received"].ToString());
            presentInstancesModel.period = record["period"].ToString();
            presentInstancesModel.created_at = record["created_at"].ToString();
            presentInstancesModel.updated_at = record["updated_at"].ToString();

            result.Add(presentInstancesModel);
        }

        return result;
    }
}
