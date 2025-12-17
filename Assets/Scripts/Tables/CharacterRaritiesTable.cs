using System;

[Serializable]
public class CharacterRaritiesModel
{
    public int id;
    public string name;
}

public class CharacterRaritiesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists character_rarities(" +
            "id int," +
            "name string," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(CharacterRaritiesModel[] characterRaritiesModel)
    {
        foreach (CharacterRaritiesModel item in characterRaritiesModel)
        {
            string query = "insert or replace into character_rarities (" +
                "id," +
                "name" +
                ")" +
                "values (" + item.id + ", \"" + item.name + "\")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    //レアリティIDが一致するレコードを取得
    public static CharacterRaritiesModel SelectId(int rarity_id)
    {
        string query = "select * from character_rarities where id = " + rarity_id;
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        CharacterRaritiesModel characterRaritiesModel = null;

        foreach (DataRow record in dataTable.Rows)
        {
            //必ずインスタンスを生成
            characterRaritiesModel = new CharacterRaritiesModel();
            characterRaritiesModel.id = int.Parse(record["id"].ToString());
            characterRaritiesModel.name = record["name"].ToString();
            break;
        }

        return characterRaritiesModel;
    }
}
