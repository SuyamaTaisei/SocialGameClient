using System;

[Serializable]
public class UsersModel
{
    public int manage_id;
    public string id;
    public string user_name;
    public int max_stamina;
    public int last_stamina;
    public string stamina_updated;
    public string last_login;
}

public static class UsersTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists users(" +
            "manage_id int," +
            "id varchar," +
            "user_name varchar," +
            "max_stamina int," +
            "last_stamina int," +
            "stamina_updated varchar," +
            "last_login varchar," +
            "primary key(manage_id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(UsersModel usersModel)
    {
        string query = "insert or replace into users (" +
            "manage_id," +
            "id," +
            "user_name," +
            "max_stamina," +
            "last_stamina," +
            "stamina_updated," +
            "last_login" +
            ")" +
            "values (" + usersModel.manage_id + ", \"" + usersModel.id + "\", \"" + usersModel.user_name + "\", " + usersModel.max_stamina + ", " + usersModel.last_stamina + ", \"" + usersModel.stamina_updated + "\", \"" + usersModel.last_login + "\")";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード取得
    public static UsersModel Select()
    {
        string query = "select * from users";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        UsersModel usersModel = new UsersModel();
        foreach (DataRow record in dataTable.Rows)
        {
            usersModel.manage_id = int.Parse(record["manage_id"].ToString());
            usersModel.id = record["id"].ToString();
            usersModel.user_name = record["user_name"].ToString();
            usersModel.max_stamina = int.Parse(record["max_stamina"].ToString());
            usersModel.last_stamina = int.Parse(record["last_stamina"].ToString());
            usersModel.stamina_updated = record["stamina_updated"].ToString();
            usersModel.last_login = record["last_login"].ToString();
        }
        return usersModel;
    }
}