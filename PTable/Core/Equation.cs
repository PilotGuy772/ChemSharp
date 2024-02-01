namespace ChemSharp.PTable.Core;

/// <summary>
/// Represents a complete chemical equation.
/// </summary>
public class Equation
{
    // the reactants and products are represented by independent lists of compounds
    // each entry into the list is a compound; elements would only be allowed if they are wrapped by a compound
    // finally, each entry also has a coefficient, which is the number of moles of that compound
    
    public Dictionary<Compound, int> Reactants { get; set; }
    public Dictionary<Compound, int> Products  { get; set; }
    
    //TODO: expand this class to include more functionality and properties
}