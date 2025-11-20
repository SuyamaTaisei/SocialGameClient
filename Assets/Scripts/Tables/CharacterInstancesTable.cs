using System;
using UnityEngine;

[Serializable]
public class CharacterInstancesModel
{
    public int id;
    public int manage_id;
    public int character_id;
    public int level;
}

public class CharacterInstancesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists character_Instances(" +
            "id int," +
            "manage_id int," +
            "character_id int," +
            "level int," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(CharacterInstancesModel characterInstancesModel)
    {
        string query = "insert or replace into character_Instances (" +
            "id," +
            "manage_id," +
            "character_id," +
            "level" +
            ")" +
            "values (" + characterInstancesModel.id + ", " + characterInstancesModel.manage_id + ", " + characterInstancesModel.character_id + ", " + characterInstancesModel.level + ")";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }
}
