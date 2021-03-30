using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;

public class DBconnector : MonoBehaviour
{
    /* NOTES FOR SCRIPT
     * - EACH TABLE NEEDS COMMAND THAT ARE ONLY NEEDED FOR ITSELF
     * 
     */

    void Start()
    {
        Debug.Log("DBconnector.Start");

        // Testing
        string[,] arrayOfTable = new string[20, 20];
        arrayOfTable = DBSelect("Location","LocationID");

        // End of Testing
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
        string[,] selectionResult = new string[6, 4];



        return selectionResult;
    }

    // PRODUCTS (Shorthand: PD)

    public string[,] DBPDSelect()
    {
        string[,] selectionResult = new string[7, 6];



        return selectionResult;
    }

    // PRODUCTLOCATION (Shorthand: PDLT)

    public string[,] DBPDLTSelect()
    {
        string[,] selectionResult = new string[24, 5];



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
