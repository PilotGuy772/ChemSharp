using ChemSharp.PTable.Core;

namespace ChemSharp.CLI;

/// <summary>
/// Basic methods for interacting with the console.
/// </summary>
public static class Console
{
    public static void ColorWrite(string content, ConsoleColor color)
    {
        ConsoleColor orig = System.Console.ForegroundColor;
        System.Console.ForegroundColor = color;
        System.Console.Write(content);
        System.Console.ForegroundColor = orig;
    }

    public static void PrintAtom(Atom atom)
    {
        //if name is longer than 13 characters, make width name length + 2; else, width is 15
        
    }
}