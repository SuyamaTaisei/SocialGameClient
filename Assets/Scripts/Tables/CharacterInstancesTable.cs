using System;
using System.Collections.Generic;

[Serializable]
public class CharacterInstancesModel
{
    public int id;
    public int manage_id;
    public int character_id;
    public int level;
}

public class CharacterInstancesTable
{
    //テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists character_Instances(" +
            "id int," +
            "manage_id int," +
            "character_id int," +
            "level int," +
            "primary key(id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    //レコード挿入
    public static void Insert(CharacterInstancesModel[] characterInstancesModel)
    {
        foreach (CharacterInstancesModel item in characterInstancesModel)
        {
            string query = "insert or replace into character_Instances (" +
                "id," +
                "manage_id," +
                "character_id," +
                "level" +
                ")" +
                "values (" + item.id + ", " + item.manage_id + ", " + item.character_id + ", " + item.level + ")";
            SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
            sqlDB.ExecuteNonQuery(query);
        }
    }

    //全レコード取得
    public static List<CharacterInstancesModel> SelectAll()
    {
        string query = "select * from character_Instances";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtility.Const.SQLITE_DB_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        List<CharacterInstancesModel> result = new List<CharacterInstancesModel>();

        foreach (DataRow record in dataTable.Rows)
        {
            CharacterInstancesModel characterInstancesModel = new CharacterInstancesModel();
            characterInstancesModel.id = int.Parse(record["id"].ToString());
            characterInstancesModel.manage_id = int.Parse(record["manage_id"].ToString());
            characterInstancesModel.character_id = int.Parse(record["character_id"].ToString());
            characterInstancesModel.level = int.Parse(record["level"].ToString());

            result.Add(characterInstancesModel);
        }

        return result;
    }
}
