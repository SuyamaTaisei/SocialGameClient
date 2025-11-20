using System;
using UnityEngine;

[Serializable]
public class ItemInstancesModel
{
    public int id;
    public int manage_id;
    public int item_id;
    public int amount;
}

public class ItemInstacesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists item_Instances(" +
            "id int," +
            "manage_id int," +
            "item_id int," +
            "amount int," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(ItemInstancesModel itemInstancesModel)
    {
        string query = "insert or replace into item_Instances (" +
            "id," +
            "manage_id," +
            "item_id," +
            "amount" +
            ")" +
            "values (" + itemInstancesModel.id + ", " + itemInstancesModel.manage_id + ", " + itemInstancesModel.item_id + ", " + itemInstancesModel.amount + ")";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }
}
