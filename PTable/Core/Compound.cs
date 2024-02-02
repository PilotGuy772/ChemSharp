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
    
    public int Coefficient { get; set; }
    
    public List<KeyValuePair<IBondable, int>> Components { get; set; }

    public Compound(params (IBondable, int)[] components)
    {
        // this constructor does NOT handle bonding!
        // that is handled by relevant methods in Calc.Bonding.*
        
        Components = new List<KeyValuePair<IBondable, int>>();
        foreach (var (component, quantity) in components)
        {
            Components.Add(new KeyValuePair<IBondable, int>(component, quantity));
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

    
    /// <summary>
    /// Adds the given IBondable to the compound with the given quantity.
    /// </summary>
    /// <param name="item">The IBondable object to add to the compound</param>
    /// <param name="quantity">The subscript of the IBondable object</param>
    public void AddComponent(IBondable item, int quantity)
    {
        // use LINQ to check if the item is already in the list
        // if it is, then update the quantity; else, add it to the list
        var existing = Components.FirstOrDefault(x => x.Key == item);
        if (existing.Key != null)
        {
            existing = new KeyValuePair<IBondable, int>(existing.Key, existing.Value + quantity);
        }
        else
        {
            Components.Add(new KeyValuePair<IBondable, int>(item, quantity));
        }

        MolarMass += item.MolarMass * quantity;
        Charge += item.Charge * quantity;
    }
}