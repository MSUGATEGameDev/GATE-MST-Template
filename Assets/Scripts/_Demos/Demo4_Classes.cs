using UnityEngine;

public class Demo4_Classes // CLASSES allow you to build complex objects with their own internal variables and functions.
{
    string hiddenWords = "Shh! It's a secret.";             // Variables are PRIVATE by default. PRIVATE variables can only be used inside the class object.
    public string notSoHiddenWords = "Nothing to hide.";    // PUBLIC variables can be seen and modified outside the object.

    bool GimmeAFalse()                                      // Functions are private by default. PRIVATE functions can only be used inside the object.
    {
        return false;
    }
    public bool GimmeATrue()                                // Public functions can be called from outside the class.
    {
        return true;
    }

    Demo4_Classes tinyClass;                        // Classes can be variables, even within their own class (creating a NESTED class).
    
    void AdjustTinyClass()
    {
        tinyClass = new();
        string readTheWords = tinyClass.notSoHiddenWords;   // A period (.) after an instance of a class allow you to reference its variables.
        bool isTrue = tinyClass.GimmeATrue();               // You can call its functions in a similar manner.
    }

    public static string sharedWords = "We're all Demo4_Classes-es";    // A STATIC variable is shared between all instances of a class and doesn't even need an instance.
    public static void DoSomething()                                    // Static functions work the same way.
    {
        string newWords = Demo4_Classes.sharedWords;                    // You call them by referencing classes, not instances.
    }
}
