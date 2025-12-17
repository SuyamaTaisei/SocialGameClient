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

    //アイテムIDが一致するレコードを取得
    public static ItemDataModel SelectId(int item_id)
    {
        string query = "select * from item_data where id = " + item_id;
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        ItemDataModel itemDataModel = null;

        foreach (DataRow record in dataTable.Rows)
        {
            //必ずインスタンスを生成
            itemDataModel = new ItemDataModel();
            itemDataModel.id = int.Parse(record["id"].ToString());
            itemDataModel.rarity_id = int.Parse(record["rarity_id"].ToString());
            itemDataModel.item_category = int.Parse(record["item_category"].ToString());
            itemDataModel.name = record["name"].ToString();
            itemDataModel.description = record["description"].ToString();
            itemDataModel.value = int.Parse(record["value"].ToString());
            break;
        }

        return itemDataModel;
    }
}
