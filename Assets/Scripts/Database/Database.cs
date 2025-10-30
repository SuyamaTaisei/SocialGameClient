using UnityEngine;
using System.Collections;

public class Database : MonoBehaviour
{

    // Use this for initializationa
    void Start()
    {
        // Insert
        SqliteDatabase sqlDB = new SqliteDatabase("config.db");
        string query = "insert into user values(1, 'Yamada', 19, 'Tokyo')";
        sqlDB.ExecuteNonQuery(query);

        // Select
        string selectQuery = "select * from user";
        DataTable dataTable = sqlDB.ExecuteQuery(selectQuery);

        string name = "";
        foreach (DataRow dr in dataTable.Rows)
        {
            name = (string)dr["name"];
            Debug.Log("name:" + name);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}