using ChemSharp.PTable.Core;
using Microsoft.Data.Sqlite;

namespace ChemSharp.PTable.Database;

/// <summary>
/// Manages entering data into the database.
/// If a database file is not provided, the periodic table data will have to be entered manually.
/// In the future, an up-to-date database will always be packaged with the application.
/// </summary>
public static class DataEntry
{
    private static SqliteCommand _command;
    
    public static void DataEntryFlow()
    {
        //uses the Linux default directory for the time being; checks will be made in the future to determine the OS
        //but actually for development it just puts the database in the current directory
        SqliteConnection connection = Connection.Connect("./ptable.db");
        
        // Open the connection
        connection.Open();
        _command = connection.CreateCommand();

        // Create a table if it doesn't exist
        // schema: atomic_number INTEGER PRIMARY KEY, name TEXT, symbol TEXT, atomic_weight REAL, period INTEGER, group INTEGER, type TEXT (with CHECK)
        _command.CommandText = "CREATE TABLE IF NOT EXISTS elements (" +
                               "atomic_number INTEGER PRIMARY KEY, " +
                               "name TEXT, " +
                               "symbol TEXT, " +
                               "atomic_weight REAL, " +
                               "period INTEGER, " +
                               "group INTEGER, " +
                               "type TEXT CHECK (type IN ('alkali metal', 'alkaline earth metal', 'transition metal', 'post-transition metal', 'metalloid', 'nonmetal', 'noble gas')))";
        _command.ExecuteNonQuery();
        
        // Prerequisites are complete, now start the interactive flow.
        // This flow is started by the user, and will be a series of prompts to enter data into the database.
        /*
         * PROCESS:
         * 1. Ask for the atomic number
         * 2. Ask for the name
         * 3. Ask for the symbol
         * 4. Ask for the atomic weight
         * 5. Ask for the group
         * 6. Ask for the period
         * 7. Present a list of types to choose from and ask for the type
         * 7.5. Once done, show a completed entry to the user and ask for confirmation
         * 8. Prepare a SQL statement to insert the data into the database
         * 9. Ask if the user wants to enter another element, else exit the flow
         *
         * Continue this in a while loop until done. Transactions are committed at the end of the flow; SQLite statements are stored in memory until then
         */

        Console.WriteLine("Database connection established. Starting data entry flow.");

        int atomicNumber;
        string name;
        string symbol;
        double atomicWeight;
        int period;
        int group;
        ElementType type;
        
        CLI.Console.ColorWrite("\nEnter the atomic number: ", ConsoleColor.Blue);
        atomicNumber = int.Parse(System.Console.ReadLine()!);
        CLI.Console.ColorWrite("\nEnter the name: ", ConsoleColor.Blue);
        name = System.Console.ReadLine()!;
        CLI.Console.ColorWrite("\nEnter the atomic symbol: ", ConsoleColor.Blue);
        symbol = System.Console.ReadLine()!;
        CLI.Console.ColorWrite("\nEnter the adjusted atomic weight: ", ConsoleColor.Blue);
        atomicWeight = double.Parse(System.Console.ReadLine()!);
        CLI.Console.ColorWrite("\nEnter the period: ", ConsoleColor.Blue);
        period = int.Parse(System.Console.ReadLine()!);
        CLI.Console.ColorWrite("\nEnter the group: ", ConsoleColor.Blue);
        group = int.Parse(System.Console.ReadLine()!);
        CLI.Console.ColorWrite("The following types of elements are supported:\n" +
                               "1. Alkali Metal\n" +
                               "2. Alkaline Earth Metal\n" +
                               "3. Transition Metal\n" +
                               "4. Post-Transition Metal\n" +
                               "5. Metalloid\n" +
                               "6. Nonmetal\n" +
                               "7. Noble Gas\n" +
                               "Enter the number corresponding to the type: ", ConsoleColor.Cyan);
        type = (ElementType)int.Parse(System.Console.ReadLine()!);
        
        // now the data is gathered. Present it to the user and ask for confirmation.
        Console.WriteLine("Here is the completed entry:");
        
        // format a nice looking entry
        
    }
}