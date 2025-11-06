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
    public static void Insert(UsersModel userModel)
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
            "values (" + userModel.manage_id + ", \"" + userModel.id + "\", \"" + userModel.user_name + "\", " + userModel.max_stamina + ", " + userModel.last_stamina + ", \"" + userModel.stamina_updated + "\", \"" + userModel.last_login + "\")";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード取得
    public static UsersModel Select()
    {
        string query = "select * from users";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        UsersModel userModel = new UsersModel();
        foreach (DataRow record in dataTable.Rows)
        {
            userModel.manage_id = int.Parse(record["manage_id"].ToString());
            userModel.id = record["id"].ToString();
            userModel.user_name = record["user_name"].ToString();
            userModel.max_stamina = int.Parse(record["max_stamina"].ToString());
            userModel.last_stamina = int.Parse(record["last_stamina"].ToString());
            userModel.stamina_updated = record["stamina_updated"].ToString();
            userModel.last_login = record["last_login"].ToString();
        }
        return userModel;
    }
}