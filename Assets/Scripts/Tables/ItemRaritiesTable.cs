using System;

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

    //レアリティIDが一致するレコードを取得
    public static ItemRaritiesModel SelectId(int rarity_id)
    {
        string query = "select * from item_rarities where id = " + rarity_id;
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        ItemRaritiesModel itemRaritiesModel = null;

        foreach (DataRow record in dataTable.Rows)
        {
            //必ずインスタンスを生成
            itemRaritiesModel = new ItemRaritiesModel();
            itemRaritiesModel.id = int.Parse(record["id"].ToString());
            itemRaritiesModel.name = record["name"].ToString();
            break;
        }

        return itemRaritiesModel;
    }
}
