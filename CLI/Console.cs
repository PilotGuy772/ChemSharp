using System.Globalization;
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
        //width DOES NOT include the pipes on either side
        int width = atom.Name.Length > 10 ? atom.Name.Length + 2 : 12;
        //top & bottom
        string line = "|" + new string('-', width) + "|";
        
        //the order of properties is as follows:
        //atomic number, name, symbol, atomic weight
        // period, group, and type are not shown in this detail.
        
        System.Console.WriteLine(line);
        System.Console.WriteLine($"| {atom.AtomicNumber.ToString().PadRight(width - 2)} |");
        //the symbol is printed in blue to give it a bit of emphasis
        System.Console.Write("| ");
        ColorWrite($"{atom.Symbol.PadRight(width - 2)}", ConsoleColor.Blue);
        System.Console.WriteLine(" |");
        
        System.Console.WriteLine($"| {atom.Name.PadRight(width - 2)} |");
        
        System.Console.WriteLine($"| {atom.AtomicWeight.ToString(CultureInfo.CurrentCulture).PadRight(width - 2)} |");
        System.Console.WriteLine(line);
        
        //and that's it! the atom is printed in a nice, neat box.
    }
    
    public static string GetOneLiner (Atom atom) => 
        $"{atom.AtomicWeight}/{atom.AtomicNumber} {atom.Name} {(atom.Charge != 0 ? atom.Charge > 0 ? Math.Abs(atom.Charge) + "+" : Math.Abs(atom.Charge) + "-" : "")}";
}