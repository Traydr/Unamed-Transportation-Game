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

    float CalcNewAvg(float currentAvg, int currentMumItems, float newValue, int newNumItem) // I hate this function
    {
        float numAvg = 0;

        numAvg = ((currentAvg * currentMumItems) + (newValue * newNumItem)) / (currentMumItems * newNumItem);

        if (numAvg < 0)
        {
            numAvg = 0f;
        }
        return numAvg;
    }

    // General Functions End


    // SPECIFIC TABLES
    // LOCATION (Shorthand: LT)

    public static string[,] DataBaseLocationSelect()
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

    public static string[,] DataBaseProductsSelect(string colName, string whereValue)
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

    public static string[,] DataBaseProductLocationSelect(string colName, string whereValue)
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

    public static void DataBaseProductLocationUpdate(string setCol, string setVal, string arCol1, string arVal1, string arCol2, string arVal2)
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

    public static string[,] DataBaseProductChangesSelect(string colName, string whereValue)
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

    public void DataBaseProductChangesInsert(int productID, int locationID, float newPrice, int newStock)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        int currentTimeHours = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        int changeID = 0; changeID = DataBaseProductChangesGetMaxChangeID() + 1;

        string combinedCommand = string.Format("INSERT INTO ProductChanges (ChangeID, ProductID, LocationID, NewPrice, NewStock, InGameTimeHours) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", changeID, productID, locationID, newPrice, newStock, currentTimeHours);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public int DataBaseProductChangesGetMaxChangeID()
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

    public string[] DataBasePlayerInventorySelect(string colName, string whereValue)
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

    public string[] DataBasePlayerInventorySelectForInventoryMenu(string productID)
    {
        string[] combinedSelect = new string[4];
        
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("select Product.ProductName, Product.BasePrice, PlayerInventory.LastPriceAVG, PlayerInventory.Stock FROM PlayerInventory, Product WHERE PlayerInventory.ProductID = {0} AND Product.ProductID = {0} ORDER BY ProductID ASC", productID);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            combinedSelect[0] = sqlReader.GetString(0);
            combinedSelect[1] = Convert.ToString(sqlReader.GetFloat(1));
            combinedSelect[2] = Convert.ToString(sqlReader.GetFloat(2));
            combinedSelect[3] = Convert.ToString(sqlReader.GetInt32(3));
        }
        
        connection.Close();
        return combinedSelect;
    }

    public void DataBasePlayerInventoryInput(int productID, float priceAvg, int stock, bool isWithinPlayer, bool isSelling)
    {
        string[] invList = new string[4];
        invList = DataBasePlayerInventorySelect("ProductID", Convert.ToString(productID));

        if (invList[0] is null)
        {
            DataBasePlayerInventoryInsert(productID, priceAvg, stock, isWithinPlayer);
        }
        else
        {
            if (isSelling == true)
            {
                if (stock == int.Parse(invList[2]))
                {
                    DataBasePlayerInventoryUpdate("LastPriceAVG", "0", "ProductID", Convert.ToString(productID));
                    DataBasePlayerInventoryUpdate("Stock", "0", "ProductID", Convert.ToString(productID));
                }
                else
                {
                    int tempStock = 0 - stock;
                    float newPriceAvg = CalcNewAvg(float.Parse(invList[1]), int.Parse(invList[2]), priceAvg, tempStock); // I hate this function
                    int newStock = int.Parse(invList[2]) - stock;

                    DataBasePlayerInventoryUpdate("LastPriceAVG", Convert.ToString(newPriceAvg), "ProductID", Convert.ToString(productID));
                    DataBasePlayerInventoryUpdate("Stock", Convert.ToString(newStock), "ProductID", Convert.ToString(productID));
                }
            }
            else
            {
                float newPriceAvg = CalcNewAvg(int.Parse(invList[1]), int.Parse(invList[2]), priceAvg, stock);
                int newStock = int.Parse(invList[2]) + stock;

                DataBasePlayerInventoryUpdate("LastPriceAVG", Convert.ToString(newPriceAvg), "ProductID", Convert.ToString(productID));
                DataBasePlayerInventoryUpdate("Stock", Convert.ToString(newStock), "ProductID", Convert.ToString(productID));
            }
        }
    }

    // I guess I have to search through the ids to find it and then output from that id
    private void DataBasePlayerInventoryUpdate(string setCol, string setVal, string arCol1, string arVal1) // SQL Error
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("UPDATE PlayerInventory SET {0} = {1} WHERE {2} = {3}", setCol, setVal, arCol1, arVal1);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    private void DataBasePlayerInventoryInsert(int productID, float priceAVG, int stock, bool pOrW)
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

    public string[,] DataBaseUpgradesSelect(string colName, string whereValue)
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

    public void DataBaseUpgradesUpdate(string setCol, string setVal, string arCol1, string arVal1)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("UPDATE Upgrades SET {0} = {1} WHERE {2} = {3}", setCol, setVal, arCol1, arVal1);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    // MutiTable Queries
}
