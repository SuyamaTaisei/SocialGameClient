using System;

[Serializable]
public class ShopRewardsModel
{
    public int id;
    public int product_id;
    public int item_id;
    public int amount;
}

public class ShopRewardsTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists shop_rewards(" +
            "id int," +
            "product_id int," +
            "item_id int," +
            "amount int," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(ShopRewardsModel[] shopRewardsModel)
    {
        foreach (ShopRewardsModel item in shopRewardsModel)
        {
            string query = "insert or replace into shop_rewards (" +
                "id," +
                "product_id," +
                "item_id," +
                "amount" +
                ")" +
                "values (" + item.id + ", " + item.product_id + ", " + item.item_id + ", " + item.amount + ")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }
}