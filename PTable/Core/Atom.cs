namespace ChemSharp.PTable.Core;

/// <summary>
/// Base class for an atom. Can also be an ion if charge is not zero.
/// </summary>
public class Atom
{
    public string Name { get; private set; }
    public string Symbol { get; private set; }
    public int AtomicNumber { get; private set; }
    public double AtomicWeight { get; private set; }

    public int Period { get; private set; }
    public int Group { get; private set; }
    public int Charge { get; set; }

    public Atom(string name, string symbol, int atomicNumber, double atomicWeight, int period, int group, int charge = 0)
    {
        Name = name;
        Symbol = symbol;
        AtomicNumber = atomicNumber;
        AtomicWeight = atomicWeight;
        Period = period;
        Group = group;
        Charge = charge;
    }
}