namespace ChemSharp.PTable.Core;

/// <summary>
/// Represents a compound. This could also be a molecule, polyatomic ion, or anything else that is made up of atoms.
/// </summary>
public class Compound : IBondable
{
    // this class stores a list of its member components and their quantities
    // a member could be an atom or another compound (in case of a polyatomic ion)

    public int Charge { get; set; }
    public double MolarMass { get; set; }
    
    public StateOfMatter State { get; set; }
    
    public Dictionary<IBondable, int> Components { get; set; }

    public Compound(params (IBondable, int)[] components)
    {
        // this constructor does NOT handle bonding!
        // that is handled by relevant methods in Calc.Bonding.*
        
        Components = new Dictionary<IBondable, int>();
        foreach (var (component, quantity) in components)
        {
            Components.Add(component, quantity);
        }
        
        foreach (var (component, quantity) in Components)
        {
            MolarMass += component.MolarMass * quantity;
        }
        
        // the charge of a compound is the sum of the charges of its components
        foreach (var (component, quantity) in Components)
        {
            Charge += component.Charge * quantity;
        }
        
        // the state of matter gets a bit complicated; we'll save that for the logic-oriented classes to handle at a later time.
        // state of matter remains unset for now.
    }
}