using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;

public class DBconnector : MonoBehaviour
{
    

    void Start()
    {
        Debug.Log("DBconnector.Start");
    }

    void DBInsert(string table, string columnName, string dataIn)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, columnName, dataIn);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    void DBDelete(string table, string columnName, string dataToDelete)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("DELETE FROM {0} WHERE {1} = {2}", table, columnName, dataToDelete);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();

    }

    public string[,] DBSelect(string table, string columnName)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();
        
        //SELECT STATEMENT
        string combinedCommand = string.Format("SELECT {0} FROM {1}", columnName, table);
        cmd.CommandText = combinedCommand;

        // SQL DATA READER
        SQLiteDataReader sqlReader = cmd.ExecuteReader();
        string[,] arrayOfTable = new string[sqlReader.FieldCount,10];
        int indexCounter = 0;

        while (sqlReader.Read())
        {
            Debug.Log(sqlReader.GetString(0));
        }

        cmd.ExecuteNonQuery();
        connection.Close();

        return arrayOfTable;
    }

    void DBUpdate(string table, string columnID, string columnUpdate, string id, string dataUpdate)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", table, columnUpdate, dataUpdate, columnID, id);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }
}
