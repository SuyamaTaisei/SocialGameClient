using System;
using UnityEngine;

[Serializable]
public class ItemCategoriesModel
{
    public int category;
    public string name;
}

public class ItemCategoriesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists item_categories(" +
            "category int," +
            "name string," +
            "primary key(category))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(ItemCategoriesModel[] itemCategoriesModel)
    {
        foreach (ItemCategoriesModel item in itemCategoriesModel)
        {
            string query = "insert or replace into item_categories (" +
                "category," +
                "name" +
                ")" +
                "values (" + item.category + ", \"" + item.name + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
