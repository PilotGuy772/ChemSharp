using ChemSharp.PTable.Core;

namespace ChemSharp.CLI;

/// <summary>
/// handles parsing chemical formulas and equations into usable objects.
/// </summary>
public class Parser
{
    public Compound ParseCompound(string formula)
    {
        // this method will parse a chemical formula into a Compound object
        // the formula will be in the form of a string, and the method will return a Compound object
        // the method will NOT check for the validity of the formula; it will assume that the formula is valid and take it at face value
        // it's the user's responsibility to ensure that the formula is valid; we're all chemists here, right?
        
        // some examples of valid formulas:
        // H2O, NaCl, Bi2(SO4)3, CaSO4, Ag2S2O3, etc.
        
        // The implementation of this method will rely on a token-based approach
        // each element is a single token, and each number is a token
        // a token may be an element, a number, a parenthesis, or a polyatomic
        // the method will iterate through the formula, tokenizing it, and then parse the tokens into a Compound object
        // now the trouble comes with determining what groups of elements are polyatomics and what are just parts of the top-level compound
        // that is trouble for another time, though. The initial implementation will require that polyatomics be wrapped in parentheses, regardless of subscript.
        
        
    }
}