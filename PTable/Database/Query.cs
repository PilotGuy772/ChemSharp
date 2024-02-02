using ChemSharp.PTable.Core;
using Microsoft.Data.Sqlite;

namespace ChemSharp.PTable.Database;

/// <summary>
/// Permits queries to the database to retrieve data.
/// </summary>
public static class Query
{
    // this connection & command is created whenever this class is used and persists for the duration of the program
    private static SqliteConnection? _connection;
    private static SqliteCommand? _command;
    
    public static Atom GetAtomByNumber(int atomicNumber)
    {
        // queries the database to find all data from the row indexed by the given atomic number
        // then returns a new Atom object with the data
        
        // if the connection is null, create a new connection
        if (_connection == null)
        {
            _connection = Connection.Connect("/Users/laeth/RiderProjects/ChemSharp/ptable.db");
            _connection.Open();
            _command = _connection.CreateCommand();
        }
        
        _command!.CommandText = "SELECT 1 FROM elements WHERE atomic_number = @atomicNumber";
        _command.Parameters.AddWithValue("@atomicNumber", atomicNumber);
        var reader = _command.ExecuteReader();
        
        // if the reader has no rows, the atomic number does not exist in the database
        if (!reader.HasRows)
        {
            throw new ArgumentException("The atomic number does not exist in the database.");
        }
        
        // if the reader has rows, read the data and create a new Atom object
        reader.Read();
        var atom = new Atom(
            reader.GetString(1),
            reader.GetString(2),
            reader.GetInt32(0),
            reader.GetDouble(3),
            reader.GetInt32(4),
            reader.GetInt32(5)
        );
        
        // close the reader and return the atom
        reader.Close();
        return atom;
    }

    public static Atom GetAtomBySymbol(string symbol)
    {
        // queries the database to find all data from the row indexed by the given symbol
        // then returns a new Atom object with the data
        
        // if the connection is null, create a new connection
        if (_connection == null)
        {
            _connection = Connection.Connect("/home/laeth/RiderProjects/ChemSharp/ptable.db");
            _connection.Open();
            _command = _connection.CreateCommand();
        }
        
        _command!.CommandText = "SELECT 1 FROM elements WHERE symbol = @symbol";
        _command.Parameters.AddWithValue("@symbol", symbol);
        var reader = _command.ExecuteReader();
        
        // if the reader has no rows, the symbol does not exist in the database
        if (!reader.HasRows)
        {
            throw new ArgumentException("The symbol does not exist in the database.");
        }
        
        // if the reader has rows, read the data and create a new Atom object
        reader.Read();
        
        var atom = new Atom(
            reader.GetString(1),
            reader.GetString(2),
            reader.GetInt32(0),
            reader.GetDouble(3),
            reader.GetInt32(4),
            reader.GetInt32(5)
        );
        
        // close the reader and return the atom
        reader.Close();
        return atom;
    }

    public static Compound GetPolyatomicIonByFormula(string formula)
    {
        // this is a simple method to return the first row that has the given formula from the polyatomics table
        
        // if the connection is null, create a new connection
        if (_connection is null)
        {
            _connection = Connection.Connect("/Users/laeth/RiderProjects/ChemSharp/ptable.db");
            _connection.Open();
            _command = _connection.CreateCommand();
        }
        
        _command!.CommandText = "SELECT 1 FROM polyatomics WHERE formula = @formula";
        _command.Parameters.AddWithValue("@formula", formula);
        var reader = _command.ExecuteReader();
        
        // if the reader has no rows, the formula does not exist in the database
        if (!reader.HasRows)
        {
            throw new ArgumentException("The formula does not exist in the database.");
        }
        
        // if the reader has rows, read the data and create a new Compound object
        reader.Read();
        
        
        //when it comes to making the compound, we will need to parse the formula.
    }
}