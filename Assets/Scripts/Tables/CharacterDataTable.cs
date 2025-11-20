using System;
using UnityEngine;

[Serializable]
public class CharacterDataModel
{
    public int id;
    public int rarity_id;
    public int character_category;
    public string name;
}

public class CharacterDataTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists character_data(" +
            "id int," +
            "rarity_id int," +
            "character_category int," +
            "name string," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(CharacterDataModel[] characterDataModel)
    {
        foreach (CharacterDataModel item in characterDataModel)
        {
            string query = "insert or replace into character_data (" +
                "id," +
                "rarity_id," +
                "character_category," +
                "name" +
                ")" +
                "values (" + item.id + ", " + item.rarity_id + ", " + item.character_category + ", \"" + item.name + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
