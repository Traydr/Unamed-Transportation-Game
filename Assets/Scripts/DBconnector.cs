using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;
using System;

public class DBconnector : MonoBehaviour
{
    public GameObject gameHandler;

    void Start()
    {
        Debug.Log("DBconnector.Start");
    }

    // General Function Start

    float CalcNewAVG(float avg, int numItems, float newValue, int newNumItem)
    {
        float numAVG = 0;

        numAVG = ((avg * numItems) + (newValue * newNumItem)) / (numItems * newNumItem);

        if (numAVG < 0)
        {
            numAVG = 0f;
        }
        else { }

        return numAVG;
    }

    // General Functions End


    // SPECIFIC TABLES
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
            selectionResult[indexCounter, 0] = Convert.ToString(sqlReader.GetInt32(0));
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
            selectionResult[indexCounter, 0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[indexCounter, 1] = sqlReader.GetString(1);
            selectionResult[indexCounter, 2] = Convert.ToString(sqlReader.GetFloat(2));
            selectionResult[indexCounter, 3] = Convert.ToString(sqlReader.GetFloat(3));
            selectionResult[indexCounter, 4] = Convert.ToString(sqlReader.GetFloat(4));
            selectionResult[indexCounter, 5] = Convert.ToString(sqlReader.GetFloat(5));
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

    public void DBPDLTUpdate(string setCol, string setVal, string arCol1, string arVal1, string arCol2, string arVal2)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("UPDATE ProductLocation SET {0} = {1} WHERE {2} = {3} AND {4} = {5}", setCol, setVal, arCol1, arVal1, arCol2, arVal2);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }


    // PRODUCTCHANGES (Shorthand: PDCH)

    public string[,] DBPDCHSelect(string colName, string whereValue)
    {
        string[,] selectionResult = new string[50, 6]; int indexCounter = 0;

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT * FROM ProductChanges WHERE {0} = {1} ORDER BY ProductID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            selectionResult[indexCounter, 0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[indexCounter, 1] = Convert.ToString(sqlReader.GetInt32(1));
            selectionResult[indexCounter, 2] = Convert.ToString(sqlReader.GetInt32(2));
            selectionResult[indexCounter, 3] = Convert.ToString(sqlReader.GetFloat(3));
            selectionResult[indexCounter, 4] = Convert.ToString(sqlReader.GetInt32(4));
            selectionResult[indexCounter, 5] = Convert.ToString(sqlReader.GetInt32(5));
            indexCounter += 1;
        }

        connection.Close();

        return selectionResult;
    }

    public void DBPDCHInsert(int productID, int locationID, float newPrice, int newStock)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        int currentTimeHours = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        int changeID = 0; changeID = DBPDCHGetMaxChangeID() + 1;

        string combinedCommand = string.Format("INSERT INTO ProductChanges (ChangeID, ProductID, LocationID, NewPrice, NewStock, InGameTimeHours) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", changeID, productID, locationID, newPrice, newStock, currentTimeHours);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public int DBPDCHGetMaxChangeID()
    {
        int maxChangeID = 0;

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT MAX(ChangeID) FROM ProductChanges");
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            maxChangeID = sqlReader.GetInt32(0); 
        }

        connection.Close();

        return maxChangeID;
    }


    // PLAYER (Shorthand: PL)
    // IMPORTANT: CITIES DONT NEED STOCK

    public string[] DBPLSelect(string colName, string whereValue)
    {
        string[] selectionResult = new string[4];

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT * FROM PlayerInventory WHERE {0} = {1} ORDER BY ProductID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            selectionResult[0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[1] = Convert.ToString(sqlReader.GetFloat(1));
            selectionResult[2] = Convert.ToString(sqlReader.GetInt32(2));
            selectionResult[3] = Convert.ToString(sqlReader.GetValue(3));
        }

        connection.Close();
        return selectionResult;
    }

    public void DBPLInput(int productID, float priceAVG, int stock, bool pOrW, bool sellT_or_BuyF)
    {
        string[] invList = new string[4];
        invList = DBPLSelect("ProductID", Convert.ToString(productID));

        if (invList[0] is null)
        {
            DBPLInsert(productID, priceAVG, stock, pOrW);
        }
        else
        {
            if (sellT_or_BuyF == true)
            {
                if (stock == int.Parse(invList[2]))
                {
                    DBPLUpdate("LastPrceAVG", "0", "ProductID", Convert.ToString(productID));
                    DBPLUpdate("Stock", "0", "ProductID", Convert.ToString(productID));
                }
                else
                {
                    float newPriceAVG = CalcNewAVG(int.Parse(invList[1]), int.Parse(invList[2]), priceAVG, -stock);
                    int newStock = int.Parse(invList[2]) - stock;

                    DBPLUpdate("LastPriceAVG", Convert.ToString(newPriceAVG), "ProductID", Convert.ToString(productID));
                    DBPLUpdate("Stock", Convert.ToString(newStock), "ProductID", Convert.ToString(productID));
                }
            }
            else
            {
                float newPriceAVG = CalcNewAVG(int.Parse(invList[1]), int.Parse(invList[2]), priceAVG, stock);
                int newStock = int.Parse(invList[2]) + stock;

                DBPLUpdate("LastPrceAVG", Convert.ToString(newPriceAVG), "ProductID", Convert.ToString(productID));
                DBPLUpdate("Stock", Convert.ToString(newStock), "ProductID", Convert.ToString(productID));
            }
        }
    }

    void DBPLUpdate(string setCol, string setVal, string arCol1, string arVal1) // SQL Error
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("UPDATE PlayerInventory SET {0} = {1} WHERE {2} = {3}", setCol, setVal, arCol1, arVal1);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    void DBPLInsert(int productID, float priceAVG, int stock, bool pOrW)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("INSERT INTO PlayerInventory (ProductID, LastPriceAVG, Stock, PlayerT_or_WarehouseF) VALUES ({0}, {1}, {2}, {3})", productID, priceAVG, stock, pOrW);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }


    // UPGRADES (Shorthand: UG)

    public string[,] DBUPSelect(string colName, string whereValue)
    {
        string[,] selectionResult = new string[10, 7];

        int indexCounter = 0;

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT * FROM Upgrades WHERE {0} = {1} ORDER BY ProductID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            selectionResult[indexCounter, 0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[indexCounter, 1] = sqlReader.GetString(1);
            selectionResult[indexCounter, 2] = Convert.ToString(sqlReader.GetFloat(2));
            selectionResult[indexCounter, 3] = Convert.ToString(sqlReader.GetValue(3));
            selectionResult[indexCounter, 4] = Convert.ToString(sqlReader.GetFloat(4));
            selectionResult[indexCounter, 5] = Convert.ToString(sqlReader.GetFloat(5));
            selectionResult[indexCounter, 6] = Convert.ToString(sqlReader.GetValue(6));
            indexCounter += 1;
        }

        connection.Close();

        return selectionResult;
    }

    public void DBUPUpdate(string setCol, string setVal, string arCol1, string arVal1)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("UPDATE Upgrades SET {0} = {1} WHERE {2} = {3}", setCol, setVal, arCol1, arVal1);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }
}
