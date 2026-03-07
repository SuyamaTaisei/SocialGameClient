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
                "values (@id, @manage_id, @present_category, @present_name, @content, @amount, @received, @period, @created_at, @updated_at)";
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"@id", item.id },
                {"@manage_id", item.manage_id },
                {"@present_category", item.present_category },
                {"@present_name", item.present_name },
                {"@content", item.content },
                {"@amount", item.amount },
                {"@received", item.received },
                {"@period", item.period },
                {"@created_at", item.created_at },
                {"@updated_at", item.updated_at },
            };
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query, param);
        }
    }

    //受取済みの有無で全レコード取得
    public static List<PresentInstancesModel> SelectAll(int received, int limit)
    {
        string query = "";

        switch(received)
        {
            case 0: query = $"select * from present_instances where received = {received} order by id desc limit {limit}"; break;
            case 1: query = $"select * from present_instances where received = {received} order by updated_at desc limit {limit}"; break;
        }

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

    //一致したプレゼントだけを取得
    public static List<PresentInstancesModel> SelectId(int InstanceId, int category, int content, int amount)
    {
        string query = $"select * from present_instances where id = {InstanceId} and present_category = {category} and content = {content} and amount = {amount}";
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

    //一度全削除してから直後にレコード全挿入
    public static void InsertFromDelete(int manageId, PresentInstancesModel[] presentInstancesModel)
    {
        string query = $"delete from present_instances where manage_id = {manageId}";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);

        foreach (PresentInstancesModel item in presentInstancesModel)
        {
            string presentQuery = "insert or replace into present_instances (" +
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
                "values (@id, @manage_id, @present_category, @present_name, @content, @amount, @received, @period, @created_at, @updated_at)";
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"@id", item.id },
                {"@manage_id", item.manage_id },
                {"@present_category", item.present_category },
                {"@present_name", item.present_name },
                {"@content", item.content },
                {"@amount", item.amount },
                {"@received", item.received },
                {"@period", item.period },
                {"@created_at", item.created_at },
                {"@updated_at", item.updated_at },
            };
            sqlDB.ExecuteNonQuery(presentQuery, param);
        }
    }
}
