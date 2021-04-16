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

    // This function is extremely annoying
    float CalcNewAvg(float currentAvg, int currentMumItems, float newValue, int newNumItem)
    {
        float numAvg = 0;
        
        if (currentMumItems + newNumItem == 0)
        {
            numAvg = 0f;
        }
        else
        {
            numAvg = ((currentAvg * currentMumItems) + (newValue * newNumItem)) / (currentMumItems + newNumItem);
        }

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

        string combinedCommand = "SELECT * FROM Location ORDER BY LocationID ASC";
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
    
    public string[] DataBaseProductLocationSelectSpecificProduct(string productId, string locationId)
    {
        string[] selectionResult = new string[5];

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT * FROM ProductLocation WHERE ProductID = {0} AND LocationID = {1} ORDER BY ProductID ASC", productId, locationId);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            selectionResult[0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[1] = Convert.ToString(sqlReader.GetInt32(1));
            selectionResult[2] = Convert.ToString(sqlReader.GetInt32(2));
            selectionResult[3] = Convert.ToString(sqlReader.GetFloat(3));
            selectionResult[4] = Convert.ToString(sqlReader.GetValue(4));
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

    public string[,] DataBaseProductChangesSelect(string colName, string whereValue)
    {
        string[,] selectionResult = new string[50, 6]; int indexCounter = 0;

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT * FROM ProductChanges WHERE {0} = {1} ORDER BY ChangeID ASC", colName, whereValue);
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
    
    // This function selects all changes with an InGameTime greater than or equal to the inputted number
    public string[,] DataBaseProductChangesSelectWithinLast24Hours(string timeSinceLastCheck)
    {
        string[,] selectionResult = new string[30, 3]; int indexCounter = 0;

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT ProductID, LocationID, NewStock FROM ProductChanges WHERE InGameTimeHours >= {0} ORDER BY ChangeID ASC", timeSinceLastCheck);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        while (sqlReader.Read())
        {
            selectionResult[indexCounter, 0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[indexCounter, 1] = Convert.ToString(sqlReader.GetInt32(1));
            selectionResult[indexCounter, 2] = Convert.ToString(sqlReader.GetInt32(2));
            indexCounter += 1;
        }

        connection.Close();

        return selectionResult;
    }

    public void DataBaseProductChangesInsert(int productId, int locationId, float newPrice, int newStock)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        int currentTimeHours = gameHandler.GetComponent<InGameTime>().GetTimeInHours();
        int changeId = 0; 
        changeId = DataBaseProductChangesGetMaxChangeID() + 1;

        string combinedCommand = string.Format("INSERT INTO ProductChanges (ChangeID, ProductID, LocationID, NewPrice, NewStock, InGameTimeHours) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", changeId, productId, locationId, newPrice, newStock, currentTimeHours);
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

        string combinedCommand = "SELECT MAX(ChangeID) FROM ProductChanges";
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

    public string[] DataBasePlayerInventorySelectForInventoryMenu(string productId)
    {
        string[] combinedSelect = new string[4];
        
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("SELECT Product.ProductName, Product.BasePrice, PlayerInventory.LastPriceAVG, PlayerInventory.Stock FROM PlayerInventory JOIN Product ON (PlayerInventory.ProductID = Product.ProductID) WHERE PlayerInventory.ProductID = {0} AND Product.ProductID = {0} ORDER BY Product.ProductID ASC", productId);
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

    public void DataBasePlayerInventoryInput(int productId, float priceAvg, int stock, bool isWithinPlayer, bool isSelling)
    {
        string[] invList = DataBasePlayerInventorySelect("ProductID", Convert.ToString(productId));

        if (invList[0] is null)
        {
            DataBasePlayerInventoryInsert(productId, priceAvg, stock, isWithinPlayer);
        }
        else
        {
            if (isSelling)
            {
                if (stock == int.Parse(invList[2]))
                {
                    DataBasePlayerInventoryUpdate("LastPriceAVG", "0", "ProductID", Convert.ToString(productId));
                    DataBasePlayerInventoryUpdate("Stock", "0", "ProductID", Convert.ToString(productId));
                }
                else
                {
                    int tempStock = 0 - stock;
                    float newPriceAvg = CalcNewAvg(float.Parse(invList[1]), int.Parse(invList[2]), priceAvg, tempStock); // I hate this function
                    int newStock = int.Parse(invList[2]) - stock;

                    DataBasePlayerInventoryUpdate("LastPriceAVG", Convert.ToString(newPriceAvg), "ProductID", Convert.ToString(productId));
                    DataBasePlayerInventoryUpdate("Stock", Convert.ToString(newStock), "ProductID", Convert.ToString(productId));
                }
            }
            else
            {
                float newPriceAvg = CalcNewAvg(float.Parse(invList[1]), int.Parse(invList[2]), priceAvg, stock); // This function call and the one above are the cause of many bugs
                int newStock = int.Parse(invList[2]) + stock;

                DataBasePlayerInventoryUpdate("LastPriceAVG", Convert.ToString(newPriceAvg), "ProductID", Convert.ToString(productId));
                DataBasePlayerInventoryUpdate("Stock", Convert.ToString(newStock), "ProductID", Convert.ToString(productId));
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

    private void DataBasePlayerInventoryInsert(int productId, float priceAvg, int stock, bool pOrW)
    {
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        string combinedCommand = string.Format("INSERT INTO PlayerInventory (ProductID, LastPriceAVG, Stock, PlayerT_or_WarehouseF) VALUES ({0}, {1}, {2}, {3})", productId, priceAvg, stock, pOrW);
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
}
