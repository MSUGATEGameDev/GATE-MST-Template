// GATE MST Game Dev Class
// Demo 1 - Variables
// Demo and syntax in C#.
// Principles apply to most languages.

using System.Collections.Generic;
                                    // Double Forward Slash (//) = COMMENT.
class Demo1_Variables                // VARIABLES store information.
{
                            
    int newVariable = 1;            // DECLARE variables: TYPE NAME = VALUE
                                    // End code lines with semicolon (;)

    int nullVariable;               // Not providing a value makes variable NULL.
                                    // Null variables will cause errors if used.
                                    // You can check for null variables by preceding with: if(VARAIBLENAME == null)

                                    // -- Basic variable types -- //
    bool thisBool = true;           // BOOL holds true/false values.
    string thisString = "Goodbye";  // STRING holds text.
    int thisInt = 4;                // INT holds whole numbers.
    float thisFloat = 11.593f;      // FLOAT holds numbers capable of decimal precision.
                                    // Manually entered numbers are ints by default. Add f to make them floats.

    public void ExampleStatements() 
    {
                                    
        thisString = "World";       // Equals (=) updates varable to the left.
                                    // TYPE only used in initial declaration. Note we just use the name from here on out.

                                    // -- Basic Math -- //
        thisInt = 4 + 3;            // Addition
        thisInt = 4 - 3;            // Subtraction
        thisInt = 4 * 3;            // Multiplication
        thisInt = 12 / 3;           // Division

   
        string newString = "Hello " + thisString;   // You can reference other variables.
                                                    // newString now equals "Hello World"       

        thisFloat += 2.4f;                          // Plus Equals (+=) adds to current value.
        thisInt++;                                  // ++ adds increments a number by one.
    }
    
}


