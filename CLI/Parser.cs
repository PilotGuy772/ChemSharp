using ChemSharp.PTable.Core;
using ChemSharp.PTable.Database;

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
        // H2O, 2NaCl, Bi2(SO4)3, 4Ca(SO4), Ag2S2O3, etc.
        
        // The implementation of this method will rely on a token-based approach
        // each element is a single token, and each number is a token
        // a token may be an element, a number, a parenthesis
        // the method will iterate through the formula, tokenizing it, and then parse the tokens into a Compound object
        // now the trouble comes with determining what groups of elements are polyatomics and what are just parts of the top-level compound
        // that is trouble for another time, though. The initial implementation will require that polyatomics be wrapped in parentheses, regardless of subscript.

        // start by initializing a new Compound object
        Compound compound = new();
        string tokenBuffer = "";
        string previousType = "";
        IBondable previousAtom;
        
        foreach (char c in formula)
        { 
            // if we see parenthesis, continue iterating until we see the close parenthesis
            // that also means that if the state is "open parenthesis", we will not do anything but
            // add the token to the token buffer until we see the close parenthesis.
            if (c == '(')
            {
                previousType = "open parenthesis";
                //explicitly exclude the parenthesis from the token buffer
            }
            else if (c == ')')
            {
                previousType = "close parenthesis";
            }
            else if (previousType == "open parenthesis")
            {
                tokenBuffer += c;
            }
            else if (char.IsDigit(c))
            {
                if (previousType is "lowercase" or "uppercase" or "close parenthesis")
                {
                    // this means that the number is a subscript for the previous token
                    // so we will add the previous token to the compound with the subscript intact
                    // and then reset the token buffer
                    
                    // now we have to set the previous atom.
                    
                    // in case of polyatomic (last type was close parenthesis), we will add the polyatomic ion to the compound
                }
                
                previousType = "number";
                tokenBuffer += c;
            }
            else if (char.IsUpper(c))
            {
                if (previousType == "number")
                {
                    // if the previous token was a number, then the token buffer is a coefficient
                    compound.Coefficient = int.Parse(tokenBuffer);
                    tokenBuffer = "";
                }
                
                //and now start a new token buffer
                //this time for a single atomic symbol
                tokenBuffer += c;
                previousType = "uppercase";
                
                //the next character is either a lowercase letter (continues atomic symbol), an open parenthesis (starts a polyatomic ion), another uppercase letter (starts another atomic symbol), or a number (starts a subscript)
                
                
            }
            else if (char.IsLower(c))
            {
                //now this is a continuation of the previous atomic symbol
                tokenBuffer += c;
                previousType = "lowercase";
                
                // the token buffer represents a complete atomic symbol
                // so let's grab it from the database
                // BUT beware that there might be a subscript following it
                // so we will keep the atom in a buffer. If the next is a subscript, we will add it to the compound with the
                // subscript intact. If it is a new atom, we will add the previous atom to the compound with a subscript of 1.
                
                
            }
            
            // if the first token(s) are numbers, update the coefficient accordingly
            if (char.IsDigit(c))
            {
                if (previousType is "lowercase" or "uppercase" or "close parenthesis")
                {
                    // this means that the number is a subscript for the previous token
                    // so we will add the previous token to the compound with the subscript intact
                    // and then reset the token buffer
                    
                    // now we have to set the previous atom.
                    
                    // 
                }
                
                previousType = "number";
                tokenBuffer += c;
            }
            else if (char.IsUpper(c))
            {
                if (previousType == "number")
                {
                    // if the previous token was a number, then the token buffer is a coefficient
                    compound.Coefficient = int.Parse(tokenBuffer);
                    tokenBuffer = "";
                }
                
                //and now start a new token buffer
                //this time for a single atomic symbol
                tokenBuffer += c;
                previousType = "uppercase";
                
                //the next character is either a lowercase letter (continues atomic symbol), an open parenthesis (starts a polyatomic ion), another uppercase letter (starts another atomic symbol), or a number (starts a subscript)
                
                
            }
            else if (char.IsLower(c))
            {
                //now this is a continuation of the previous atomic symbol
                tokenBuffer += c;
                previousType = "lowercase";
                
                // the token buffer represents a complete atomic symbol
                // so let's grab it from the database
                // BUT beware that there might be a subscript following it
                // so we will keep the atom in a buffer. If the next is a subscript, we will add it to the compound with the
                // subscript intact. If it is a new atom, we will add the previous atom to the compound with a subscript of 1.
                
                
            }

        }

        

    }

    public Compound ParsePolyatomicCompound(string formula)
    {
        //this version of the method will parse a polyatomic ion
        //this version removes all support for parenthesis because polyatomics don't contain other polyatomics

        string tokenBuffer = "";
        string previousType = "";
        Compound compound = new();
        Atom previousAtom;
        
        foreach (char c in formula)
        {
            //if the character is an UPPERCASE LETTER, it's the start of a new atomic symbol
            if (char.IsUpper(c))
            {
                //if the previous token was a number, then the token buffer is a coefficient
                //beware that these are polyatomics!!! so they should NEVER have coefficients
                if (previousType == "number")
                {
                    compound.Coefficient = int.Parse(tokenBuffer);
                    tokenBuffer = "";
                }
                
                //and now start a new token buffer
                //this time for a single atomic symbol
                tokenBuffer += c;
                previousType = "uppercase";
            }
            
            //if the character is a lowercase letter, it's a continuation of the previous atomic symbol
            else if (char.IsLower(c))
            {
                tokenBuffer += c;
                previousType = "lowercase";
            }
            
            //finally, if the character is a number, it's a subscript
            else if (char.IsDigit(c))
            {
                //if the previous token was a lowercase letter, then the token buffer is an atomic symbol
                if (previousType == "lowercase")
                {
                    //grab the atomic symbol from the database
                    previousAtom = Query.GetAtomBySymbol(tokenBuffer);
                    
                    //here's the thing though.. we can't add the atom to the compound yet
                    //because there may be more digits to the subscript
                    //so let's clear the buffer and add self
                    tokenBuffer = "";
                    tokenBuffer += c;
                }
                
                //if the previous token was a number, then we just have a multi-digit subscript
                if (previousType == "number")
                {
                    compound.Coefficient = int.Parse(tokenBuffer);
                    tokenBuffer = "";
                }
                
                //and now start a new token buffer
                //this time for a single atomic symbol
                tokenBuffer += c;
                previousType = "number";
            }
        }
    }
}