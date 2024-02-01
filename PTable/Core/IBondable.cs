namespace ChemSharp.PTable.Core;

/// <summary>
/// Represents a bondable object. This could be an atom, a compound, or anything else that can form a bond.
/// Any object that can form a bond should implement this interface.
/// </summary>
public interface IBondable
{
    // charge is required for computing stuff
    public int Charge { get; set; }
    
    // each bondable object must present its molar mass
    public double MolarMass { get; set; }
}