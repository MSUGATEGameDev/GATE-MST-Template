using UnityEngine;

public class Demo2_Functions // FUNCTIONS allow lines of code to be called from other lines of code.
{
    int thisValue = 0;
    string thingToSay = "";
    
    int AddTogether (int value1, int value2)    // Functions can have one output and many input variables.
    {
        return value1 + value2;                 // RETURN ends the function and provides the output value.
    }

    void AddAndSay()                            // VOID functions don't have outputs, they just do stuff.
    {
        int addedVariable = AddTogether(3, 7);
        thingToSay = "Three plus seven = " + addedVariable;
    }

    void Controls()
    {
        bool tfValue;           // Variables created within a function only exist until the function ends.
        if(thingToSay == "Hi")  // IF statements only run if the value is true.
        {                       // Since equals (=) is assignment, double equals (==) checks equality.
            thisValue = 19;
        }
        else if (thingToSay != "No")                // ELSE IF statements only check if the previous statement is false.
        {                                           // (!=) does not equal
            tfValue = 1 > 4;                        // (>) greater than (< less than)
            tfValue = 3 >= 6;                       // (>=) greater than or equal (<= less than or equal)
            tfValue = 4 > 6 && 5 > 9;               // (&&) and (||) or
            tfValue = (5 > 6 || 6 > 8) || 5 > 9;    // Wrap in parenthesis to establish order of operations.
        }
        else // Else runs if previous statements fail;
        {
            thisValue = 9;
        }

        switch (thisValue) // SWITCH tests for different possible variable values
        {
            case 9:
                // Do something here.
                break;
            case 6:
                // Or do something here.
                break;
            default:            // Default happens if nothing else applies.
                // Or do something here.
                break;
        }

        int counter = 0;
        while (counter < 6)         // WHILE loops check for a true/false after every time they run and loop.
        {
            counter++;
        }

        for (int i = 0; i < 6; i++) // FOR loops are built for running a specific number of times.
        { 
            // Do something.
        }
    }
}
