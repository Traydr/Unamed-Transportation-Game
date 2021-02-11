using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;

public class DBconnector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SQLiteConnection connection =
                 new SQLiteConnection(@"Data Source=C:\Users\titas\Desktop\Games & Programs\ProjectFiles\Databases\testdb.db;Version=3;");
        connection.Open();
        SQLiteCommand command = connection.CreateCommand();
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "CREATE TABLE IF NOT EXISTS 'highscores' ( " +
                          "  'id' INTEGER PRIMARY KEY, " +
                          "  'name' TEXT NOT NULL, " +
                          "  'score' INTEGER NOT NULL" +
                          ");";
        command.ExecuteNonQuery();
        connection.Close();
    }
}
