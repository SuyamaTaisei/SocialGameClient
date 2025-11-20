using System;
using UnityEngine;

[Serializable]
public class ItemRaritiesModel
{
    public int id;
    public string name;
}

public class ItemRaritiesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists item_rarities(" +
            "id int," +
            "name string," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(ItemRaritiesModel[] itemRaritiesModel)
    {
        foreach (ItemRaritiesModel item in itemRaritiesModel)
        {
            string query = "insert or replace into item_rarities (" +
                "id," +
                "name" +
                ")" +
                "values (" + item.id + ", \"" + item.name + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
