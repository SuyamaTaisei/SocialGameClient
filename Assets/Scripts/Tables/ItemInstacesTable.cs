using System;
using System.Collections.Generic;

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
    public static void Insert(ItemInstancesModel[] itemInstancesModel)
    {
        foreach (ItemInstancesModel item in itemInstancesModel)
        {
            string query = "insert or replace into item_Instances (" +
                "id," +
                "manage_id," +
                "item_id," +
                "amount" +
                ")" +
                "values (" + item.id + ", " + item.manage_id + ", " + item.item_id + ", " + item.amount + ")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    //ソート付きで全レコード取得
    public static List<ItemInstancesModel> SelectSortAll(string column, string sort)
    {
        string query = "";

        //カラム名単位でSQLクエリを呼び出し
        switch (column)
        {
            case "amount": query = $"select * from item_Instances order by {column} {sort}"; break;
            case "rarity_id": query = $"select ii.* from item_Instances as ii inner join item_data as id on id.id = ii.item_id order by id.{column} {sort}"; break;
        }

        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<ItemInstancesModel> result = new List<ItemInstancesModel>();

        foreach (DataRow record in dataTable.Rows)
        {
            ItemInstancesModel itemInstancesModel = new ItemInstancesModel();
            itemInstancesModel.id = int.Parse(record["id"].ToString());
            itemInstancesModel.manage_id = int.Parse(record["manage_id"].ToString());
            itemInstancesModel.item_id = int.Parse(record["item_id"].ToString());
            itemInstancesModel.amount = int.Parse(record["amount"].ToString());

            result.Add(itemInstancesModel);
        }

        return result;
    }
}
