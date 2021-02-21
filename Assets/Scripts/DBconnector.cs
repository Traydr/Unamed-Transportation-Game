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
 *  - REMEMBER TO CHANGE THE DIRECTORIES OF THE // SQLiteConnection // TO FINAL DATABASE LOCATION AND FILE NAME
 */

public class DBconnector : MonoBehaviour
{
    

    void Start()
    {
        // Tests for database commands, all commands should eventually get their own functions!
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\titas\Desktop\Games & Programs\ProjectFiles\Databases\testdb.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        /* Written Originally For Testing
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'highscores' ( " +
                          "  'id' INTEGER PRIMARY KEY, " +
                          "  'name' TEXT NOT NULL, " +
                          "  'score' INTEGER NOT NULL" +
                          ");";

        cmd.CommandText = "INSERT INTO highscores (id,name,score) VALUES (0,User,0)";
        */
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    void DBInsert(string table, string columnName, string dataIn)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\titas\Desktop\Games & Programs\ProjectFiles\Databases\testdb.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, columnName, dataIn);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    void DBDelete(string table, string columnName, string dataToDelete)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\titas\Desktop\Games & Programs\ProjectFiles\Databases\testdb.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("DELETE FROM {0} WHERE {1} = {2}", table, columnName, dataToDelete);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();

    }

    void DBSelect(string table, string columnName)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\titas\Desktop\Games & Programs\ProjectFiles\Databases\testdb.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT {0} FROM {1}", columnName, table);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }
}
