using System;

[Serializable]
public class ItemDataModel
{
    public int id;
    public int rarity_id;
    public int item_category;
    public string name;
    public string description;
    public int value;
}

public class ItemDataTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists item_data(" +
            "id int," +
            "rarity_id int," +
            "item_category int," +
            "name string," +
            "description string," +
            "value int," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(ItemDataModel[] itemDataModel)
    {
        foreach (ItemDataModel item in itemDataModel)
        {
            string query = "insert or replace into item_data (" +
                "id," +
                "rarity_id," +
                "item_category," +
                "name," +
                "description," +
                "value" +
                ")" +
                "values (" + item.id + ", " + item.rarity_id + ", " + item.item_category + ", \"" + item.name + "\", \"" + item.description + "\", " + item.value + ")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}
