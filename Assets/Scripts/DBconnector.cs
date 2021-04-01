using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;
using System;

public class DBconnector : MonoBehaviour
{
    /* NOTES FOR SCRIPT
     * - EACH TABLE NEEDS COMMAND THAT ARE ONLY NEEDED FOR ITSELF
     * 
     */

    void Start()
    {
        Debug.Log("DBconnector.Start");
    }

    // SECTION: COPY PASTE [START OF SECTION - TO BE DELETED]
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
        //string combinedCommand = string.Format("SELECT {0} FROM {1}", columnName, table);
        string combinedCommand = string.Format("SELECT * FROM {0} ORDER BY {1} ASC", table, columnName);
        cmd.CommandText = combinedCommand;

        // SQL DATA READER
        SQLiteDataReader sqlReader = cmd.ExecuteReader();
        string[,] arrayOfTable = new string[sqlReader.FieldCount,10];

        while (sqlReader.Read())
        {
            string logDebug = string.Format("{0}, {1}, {2}, {3}", sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3));
            Debug.Log(logDebug);
        }

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
    // END OF SECTION: COPY PASTE


    // ALL TABLES
    // LOCATION (Shorthand: LT)

    public string[,] DBLTSelect()
    {
        string[,] selectionResult = new string[6, 4]; int indexCounter = 0;

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT * FROM Location ORDER BY LocationID ASC");
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            selectionResult[indexCounter, 0] = sqlReader.GetString(0);
            selectionResult[indexCounter, 1] = sqlReader.GetString(1);
            selectionResult[indexCounter, 2] = sqlReader.GetString(2);
            selectionResult[indexCounter, 3] = sqlReader.GetString(3);
            indexCounter += 1;
        }

        connection.Close();

        return selectionResult;
    }



    // PRODUCTS (Shorthand: PD)

    public string[,] DBPDSelect(string colName, string whereValue)
    {
        string[,] selectionResult = new string[1, 6]; int indexCounter = 0;

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT * FROM Product WHERE {0} = {1} ORDER BY ProductID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            selectionResult[indexCounter, 0] = sqlReader.GetString(0);
            selectionResult[indexCounter, 1] = sqlReader.GetString(1);
            selectionResult[indexCounter, 2] = sqlReader.GetString(2);
            selectionResult[indexCounter, 3] = sqlReader.GetString(3);
            selectionResult[indexCounter, 4] = sqlReader.GetString(4);
            selectionResult[indexCounter, 5] = sqlReader.GetString(5);
            indexCounter += 1;
        }

        connection.Close();



        return selectionResult;
    }

    // PRODUCTLOCATION (Shorthand: PDLT)

    public string[,] DBPDLTSelect(string colName, string whereValue)
    {
        string[,] selectionResult = new string[24, 5]; int indexCounter = 0;

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT * FROM ProductLocation WHERE {0} = {1} ORDER BY ProductID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            selectionResult[indexCounter, 0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[indexCounter, 1] = Convert.ToString(sqlReader.GetInt32(1));
            selectionResult[indexCounter, 2] = Convert.ToString(sqlReader.GetInt32(2));
            selectionResult[indexCounter, 3] = Convert.ToString(sqlReader.GetFloat(3));
            selectionResult[indexCounter, 4] = Convert.ToString(sqlReader.GetValue(4));
            indexCounter += 1;
        }

        connection.Close();

        return selectionResult;
    }

    // PRODUCTCHANGES (Shorthand: PDCH)

    public string[,] DBPDCHSelect()
    {
        string[,] selectionResult = new string[50, 6];



        return selectionResult;
    }

    // PLAYER (Shorthand: PL)

    public string[,] DBPLSelect()
    {
        string[,] selectionResult = new string[10, 4];



        return selectionResult;
    }

    // UPGRADES (Shorthand: UG)

    public string[,] DBUPSelect()
    {
        string[,] selectionResult = new string[10, 7];



        return selectionResult;
    }
}
