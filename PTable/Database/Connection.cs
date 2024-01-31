using Microsoft.Data.Sqlite;

namespace ChemSharp.PTable.Database;

/// <summary>
/// Handles initiation and connection to the database file.
/// On Linux, the file is by default located at /usr/share/chemsharp/ptable.db
/// On DOS, the file is by default located at C:\Program Files\ChemSharp\ptable.db
/// One OSX, the file is by default located at /Applications/ChemSharp/ptable.db
/// Only Linux will be implemented initially.
/// </summary>
public class Connection
{
    
    /// <summary>
    /// Creates a connection to an existing periodic table database.
    /// </summary>
    /// <returns>A connection to the database</returns>
    public static SqliteConnection Connect(string databaseFile)
    {
        // Connect to the database
        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = databaseFile
        }.ToString();

        return new SqliteConnection(connectionString);
    }
}