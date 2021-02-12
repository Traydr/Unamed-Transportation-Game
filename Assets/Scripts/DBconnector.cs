using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;

/* TO DO LIST:
 * - ADD DELETE
 * - ADD
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

public class DBconnector : MonoBehaviour
{
    void Start()
    {
        // Tests for database commands, all commands should eventually get their own functions!
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\titas\Desktop\Games & Programs\ProjectFiles\Databases\testdb.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();


        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'highscores' ( " +
                          "  'id' INTEGER PRIMARY KEY, " +
                          "  'name' TEXT NOT NULL, " +
                          "  'score' INTEGER NOT NULL" +
                          ");";

        cmd.CommandText = "INSERT INTO highscores (id,name,score) VALUES (0,User,0)";

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    void DBInsert(string table, string tableAttributes, string dataIn)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\titas\Desktop\Games & Programs\ProjectFiles\Databases\testdb.db;Version=3;");
        connection.Open();

        SQLiteCommand cmd = connection.CreateCommand();
        string combinedCommand = "INSERT INTO ";
        combinedCommand += table + " (" + tableAttributes + ") VALUES (" + dataIn + ")";
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }
}
