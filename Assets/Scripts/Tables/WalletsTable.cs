using System;

[Serializable]
public class WalletsModel
{
    public int manage_id;
    public int coin_amount;
    public int gem_free_amount;
    public int gem_paid_amount;
}

public static class WalletsTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists wallets(" +
            "manage_id int," +
            "coin_amount int," +
            "gem_free_amount int," +
            "gem_paid_amount int," +
            "primary key(manage_id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(WalletsModel walletsModel)
    {
        string query = "insert or replace into wallets (" +
            "manage_id," +
            "coin_amount," +
            "gem_free_amount," +
            "gem_paid_amount" +
            ")" +
            "values (" + walletsModel.manage_id + ", " + walletsModel.coin_amount + ", " + walletsModel.gem_free_amount + ", " + walletsModel.gem_paid_amount + ")";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード取得
    public static WalletsModel Select()
    {
        string query = "select * from wallets";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        WalletsModel walletsModel = new WalletsModel();
        foreach (DataRow record in dataTable.Rows)
        {
            walletsModel.manage_id = int.Parse(record["manage_id"].ToString());
            walletsModel.coin_amount = int.Parse(record["coin_amount"].ToString());
            walletsModel.gem_free_amount = int.Parse(record["gem_free_amount"].ToString());
            walletsModel.gem_paid_amount = int.Parse(record["gem_paid_amount"].ToString());
        }
        return walletsModel;
    }
}