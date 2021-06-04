using UnityEngine;
using System.Data.SQLite;
using System;

public class DataBaseConnector : MonoBehaviour
{
    public GameObject gameHandler;
    private GeneralMathFunctions _gmf;

    void Start()
    {
        Debug.Log("DataBaseConnector.Start");
    }

    // SPECIFIC TABLES
    // LOCATION (Shorthand: LT)

    // SQL Select command for the table 'Location'
    public static string[,] DataBaseLocationSelect()
    {
        string[,] selectionResult = new string[6, 4]; int indexCounter = 0;

        // Creating the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creating the command and then executing
        string combinedCommand = "SELECT * FROM Location ORDER BY LocationID ASC";
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Going through each of the rows the sqlreader sends and inputing them to a 2d string
        while (sqlReader.Read())
        {
            selectionResult[indexCounter, 0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[indexCounter, 1] = sqlReader.GetString(1);
            selectionResult[indexCounter, 2] = sqlReader.GetString(2);
            selectionResult[indexCounter, 3] = sqlReader.GetString(3);
            indexCounter += 1;
        }

        // Closing the connection to the database and returning the 2d array of all items in the location table
        connection.Close();
        return selectionResult;
    }


    // PRODUCTS (Shorthand: PD)

    // SQL select command for the 'products' table
    public static string[,] DataBaseProductsSelect(string colName, string whereValue)
    {
        string[,] selectionResult = new string[1, 6]; int indexCounter = 0;

        // Creating the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creating the command and then executing
        string combinedCommand = string.Format("SELECT * FROM Product WHERE {0} = {1} ORDER BY ProductID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Going through each of the rows the sqlreader sends and inputing the variables to a 2d string (Also converting them to strings)
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

        // Closing the connection to the database and returning the 2d array of a selected item in the products table
        connection.Close();
        return selectionResult;
    }


    // PRODUCTLOCATION (Shorthand: PDLT)
    
    // A SQL select command for the 'ProductLocation' table
    public static string[,] DataBaseProductLocationSelect(string colName, string whereValue)
    {
        string[,] selectionResult = new string[24, 5]; int indexCounter = 0;

        // Creating the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creating the command and then executing in this case taking in arguments on what things to search for
        string combinedCommand = string.Format("SELECT * FROM ProductLocation WHERE {0} = {1} ORDER BY ProductID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Going throught the sqlReader and putting any data into the 2d array of strings 
        while (sqlReader.Read())
        {
            selectionResult[indexCounter, 0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[indexCounter, 1] = Convert.ToString(sqlReader.GetInt32(1));
            selectionResult[indexCounter, 2] = Convert.ToString(sqlReader.GetInt32(2));
            selectionResult[indexCounter, 3] = (sqlReader.GetFloat(3).ToString("n2"));
            selectionResult[indexCounter, 4] = Convert.ToString(sqlReader.GetValue(4));
            indexCounter += 1;
        }

        // The connection is then closed and the results are returned
        connection.Close();

        return selectionResult;
    }
    
    // SQL Select statement that takes the argument for a specific productid and locationid to return a specific row
    public string[] DataBaseProductLocationSelectSpecificProduct(string productId, string locationId)
    {
        string[] selectionResult = new string[5];

        // connection to the database is made
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creation of the sql command and the execution of the reader
        string combinedCommand = string.Format("SELECT * FROM ProductLocation WHERE ProductID = {0} AND LocationID = {1} ORDER BY ProductID ASC", productId, locationId);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Reading from the reader and then putting them in the array of strings
        while (sqlReader.Read())
        {
            selectionResult[0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[1] = Convert.ToString(sqlReader.GetInt32(1));
            selectionResult[2] = Convert.ToString(sqlReader.GetInt32(2));
            selectionResult[3] = Convert.ToString(sqlReader.GetFloat(3));
            selectionResult[4] = Convert.ToString(sqlReader.GetValue(4));
        }
        
        // Closing the connection and then returning the output of the select statement
        connection.Close();

        return selectionResult;
    }

    // SQL command to update and item in the 'ProductLocation' table
    // Takes in what item needs to be updated along side where the item is e.g. LocalPrice, 1 at ProductID, 0 and LocationID, 0
    public static void DataBaseProductLocationUpdate(string setCol, string setVal, string arCol1, string arVal1, string arCol2, string arVal2)
    {
        // Creation of the connection
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creation and execution of the sql command along with the closing of the connection
        string combinedCommand = string.Format("UPDATE ProductLocation SET {0} = {1} WHERE {2} = {3} AND {4} = {5}", setCol, setVal, arCol1, arVal1, arCol2, arVal2);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }


    // PRODUCTCHANGES (Shorthand: PDCH)

    // SQL select statement for 'ProductChanges' table, along with an argument for what exact value to search for
    public string[,] DataBaseProductChangesSelect(string colName, string whereValue)
    {
        string[,] selectionResult = new string[50, 6]; int indexCounter = 0;

        // Creating the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creating the command to be executed and then executing it through the sqlReader
        string combinedCommand = string.Format("SELECT * FROM ProductChanges WHERE {0} = {1} ORDER BY ChangeID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Reading from the reader and putting it into the 2d array
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

        // Closing the connection and then returning the output of the select statement
        connection.Close();

        return selectionResult;
    }
    
    // This SQL command selects all changes with an InGameTimeHours value that is greater than or equal to the inputted number
    // And only selecting the changes in stock as this function is only used for the new price calculations in GameEventHandler.cs
    public string[,] DataBaseProductChangesSelectWithinLast24Hours(string timeSinceLastCheck)
    {
        string[,] selectionResult = new string[100, 3]; int indexCounter = 0;

        // Creation of the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creating the command and executing it
        string combinedCommand = string.Format("SELECT ProductID, LocationID, NewStock FROM ProductChanges WHERE InGameTimeHours >= {0} ORDER BY ChangeID ASC", timeSinceLastCheck);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Reading the output from the reader and putting it into the 2d array selectionResult
        while (sqlReader.Read())
        {
            selectionResult[indexCounter, 0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[indexCounter, 1] = Convert.ToString(sqlReader.GetInt32(1));
            selectionResult[indexCounter, 2] = Convert.ToString(sqlReader.GetInt32(2));
            indexCounter += 1;
        }

        // Closing the connection and returning the array
        connection.Close();

        return selectionResult;
    }

    // SQL command to instert new rows into the 'ProductChanges' table
    // the change id and c
    public void DataBaseProductChangesInsert(int productId, int locationId, float newPrice, int newStock)
    {
        // Creating the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Getting the current time in hours to be put into table
        int currentTimeHours = gameHandler.GetComponent<GameTimeHandler>().GetTimeInHours();
        // Getting the id of the last change and then adding 1 to it to get the next empty id
        int changeId = 0; 
        changeId = DataBaseProductChangesGetMaxChangeID() + 1;

        // Creating the sql command from all the inputted values and also executing it
        string combinedCommand = string.Format("INSERT INTO ProductChanges (ChangeID, ProductID, LocationID, NewPrice, NewStock, InGameTimeHours) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", changeId, productId, locationId, newPrice, newStock, currentTimeHours);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    // SQL commandd to get the maximum changeId that is currently in the table
    public int DataBaseProductChangesGetMaxChangeID()
    {
        int maxChangeID = 0;

        // Creating the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creating the command and executing the reader
        string combinedCommand = "SELECT MAX(ChangeID) FROM ProductChanges";
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Reading from the reader then closing the connection to the database and returning the value that was outputed
        while (sqlReader.Read())
        {
            maxChangeID = sqlReader.GetInt32(0); 
        }

        connection.Close();

        return maxChangeID;
    }


    // PLAYER (Shorthand: PL)
    // IMPORTANT: CITIES DONT NEED STOCK**

    // SQL Command to select items from the 'PlayerInventory' table
    public string[] DataBasePlayerInventorySelect(string colName, string whereValue)
    {
        string[] selectionResult = new string[4];

        // Creating the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creating the command and then executing it
        string combinedCommand = string.Format("SELECT * FROM PlayerInventory WHERE {0} = {1} ORDER BY ProductID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Reading what the sql command returned and then puttting it into a array of strings
        while (sqlReader.Read())
        {
            selectionResult[0] = Convert.ToString(sqlReader.GetInt32(0));
            selectionResult[1] = Convert.ToString(sqlReader.GetFloat(1));
            selectionResult[2] = Convert.ToString(sqlReader.GetInt32(2));
            selectionResult[3] = Convert.ToString(sqlReader.GetValue(3));
        }

        // Closing the connection to the database and returning the results
        connection.Close();
        return selectionResult;
    }

    // A composite SQL command that takes data from the 'PlayerInventory' and 'Product' tables
    // This function specifically returns all the data needed for the player inventory menu in the game for a specific productID
    public string[] DataBasePlayerInventorySelectForInventoryMenu(string productId)
    {
        string[] combinedSelect = new string[4];
        
        // Creating the conection
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creating the sql command and executing
        string combinedCommand = string.Format("SELECT Product.ProductName, Product.BasePrice, PlayerInventory.LastPriceAVG, PlayerInventory.Stock FROM PlayerInventory JOIN Product ON (PlayerInventory.ProductID = Product.ProductID) WHERE PlayerInventory.ProductID = {0} AND Product.ProductID = {0} ORDER BY Product.ProductID ASC", productId);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Reading from the reader and then inputting them into an array of strings
        while (sqlReader.Read())
        {
            combinedSelect[0] = sqlReader.GetString(0);
            combinedSelect[1] = Convert.ToString(sqlReader.GetFloat(1));
            combinedSelect[2] = Convert.ToString(sqlReader.GetFloat(2));
            combinedSelect[3] = Convert.ToString(sqlReader.GetInt32(3));
        }
        
        // Closing the connection and returning the array
        connection.Close();
        return combinedSelect;
    }

    // This function determines weather the inputed data just needs to change the values in the 'PlayerInventory' table
    // Or if it needs to insert a new row
    public void DataBasePlayerInventoryInput(int productId, float priceAvg, int stock, bool isWithinPlayer, bool isSelling)
    {
        string[] invList = DataBasePlayerInventorySelect("ProductID", Convert.ToString(productId));

        // If the array does not contain anything then the item does not exist in the table yet
        // Therefore we need to insert | if it does exist then we move to else and we have to update the table
        if (invList[0] is null)
        {
            DataBasePlayerInventoryInsert(productId, priceAvg, stock, isWithinPlayer);
        }
        else
        {
            // Checks if we are in the buying or selling menu
            if (isSelling)
            {
                // If we are selling all the items we have then we dont have to bother calculating the price as it will we 0 so will stock
                if (stock == int.Parse(invList[2]))
                {
                    DataBasePlayerInventoryUpdate("LastPriceAVG", "0", "ProductID", Convert.ToString(productId));
                    DataBasePlayerInventoryUpdate("Stock", "0", "ProductID", Convert.ToString(productId));
                }
                else // In any other case we have to calculate the new price and new stock numbers
                {
                    int tempStock = 0 - stock;
                    float newPriceAvg = _gmf.GetAdditionToExistingAverage(float.Parse(invList[1]), int.Parse(invList[2]), priceAvg, tempStock); // I hate this function
                    int newStock = int.Parse(invList[2]) - stock;

                    DataBasePlayerInventoryUpdate("LastPriceAVG", Convert.ToString(newPriceAvg), "ProductID", Convert.ToString(productId));
                    DataBasePlayerInventoryUpdate("Stock", Convert.ToString(newStock), "ProductID", Convert.ToString(productId));
                }
            }
            else // If we are buying we need to calculate the new price and new stock
            {
                float newPriceAvg = _gmf.GetAdditionToExistingAverage(float.Parse(invList[1]), int.Parse(invList[2]), priceAvg, stock); // This function call and the one above are the cause of many bugs
                int newStock = int.Parse(invList[2]) + stock;

                DataBasePlayerInventoryUpdate("LastPriceAVG", Convert.ToString(newPriceAvg), "ProductID", Convert.ToString(productId));
                DataBasePlayerInventoryUpdate("Stock", Convert.ToString(newStock), "ProductID", Convert.ToString(productId));
            }
        }
    }

    // SQL Command to update an item in 'playerInventory' table
    private void DataBasePlayerInventoryUpdate(string setCol, string setVal, string arCol1, string arVal1) // SQL Error
    {
        // Creating the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creting the SQL command
        string combinedCommand = string.Format("UPDATE PlayerInventory SET {0} = {1} WHERE {2} = {3}", setCol, setVal, arCol1, arVal1);
        cmd.CommandText = combinedCommand;

        // Executing the command and then closing the connection
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    // SQL command to insert an new row into the 'PlayerInventory' table
    private void DataBasePlayerInventoryInsert(int productId, float priceAvg, int stock, bool pOrW)
    {
        // Creating the connection
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creting the SQL command
        string combinedCommand = string.Format("INSERT INTO PlayerInventory (ProductID, LastPriceAVG, Stock, PlayerT_or_WarehouseF) VALUES ({0}, {1}, {2}, {3})", productId, priceAvg, stock, pOrW);
        cmd.CommandText = combinedCommand;

        // Executing the command and then closing the connection
        cmd.ExecuteNonQuery();
        connection.Close();
    }


    // UPGRADES (Shorthand: UG)

    // SQL Command to read specific items in the 'Upgrades' table
    public string[,] DataBaseUpgradesSelect(string colName, string whereValue)
    {
        string[,] selectionResult = new string[10, 7];

        int indexCounter = 0;

        // Creating the connection
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creating the command and executing the reader
        string combinedCommand = string.Format("SELECT * FROM Upgrades WHERE {0} = {1} ORDER BY ProductID ASC", colName, whereValue);
        cmd.CommandText = combinedCommand;

        SQLiteDataReader sqlReader = cmd.ExecuteReader();

        // Reading from the reader until it doesnt have anything more and then adding them to the 2d array
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

        // Closing the connection and returning the array
        connection.Close();

        return selectionResult;
    }

    // SQL Update command for the 'Upgrades' table
    public void DataBaseUpgradesUpdate(string setCol, string setVal, string arCol1, string arVal1)
    {
        // Creates the connection to the database
        SQLiteConnection connection = new SQLiteConnection(@"Data Source=Assets/DataBase/UnamedTransportationGame.db;Version=3;");
        connection.Open();
        SQLiteCommand cmd = connection.CreateCommand();

        // Creates the command and executes it, then closes the connection
        string combinedCommand = string.Format("UPDATE Upgrades SET {0} = {1} WHERE {2} = {3}", setCol, setVal, arCol1, arVal1);
        cmd.CommandText = combinedCommand;

        cmd.ExecuteNonQuery();
        connection.Close();
    }
}
