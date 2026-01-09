// Everything to the right of a double slash on any given line is a COMMENT, which helps explain the code.

using System;
using System.Collections.Generic;
using UnityEngine;
// USING statements connect this script to other scripts and are usually auto-generated.

// CLASSES are collections of VARIABLES (which store data) and FUNCTIONS (which make things happen).
public class ThisDemoScript : MonoBehaviour // ThisDemoScript is DERIVED from Unity's MonoBehavior class, which allows us to attach it to game objects in unity.
{
    // REGIONS allow you to organize and minimize code.
    #region Variables
    #region Variable Types
    // Variables need a TYPE, a NAME, and usually a VALUE. Here are some variable types.
    bool aBool = false;                 // BOOLEANS are either true or false;
    string aString = "Hello Everyone";  // STRINGS are text;
    int anInt = 1;                      // INTEGERS are whole numbers.
    float aFloat = 1.59f;               // FLOATING POINTs can have decimals.
    
    double aDouble = 1.59;              // Doubles are big floats (more storage, more precision).
    long aLong = 5L;                    // LONGS are big ints (more storage, bigger numbers)

    List<string> aList = new List<string>(); // LISTS contain multiple values in one variable.
    Dictionary<string,int> aDict;            // DICTIONARIES are lists where values have lookup codes.

    Door aDoor; // Any class can be a variable within an existing class.
    ThisDemoScript babyScript; // You can even reference a class within itself.
    DateTime aDate; // There are lots of pre-existing classes to pull from.
    #endregion

    #region Visibility
    // Variables are PRIVATE by default. They can't be seen outside of the class. This is a good way to keep things from getting messy.
    public string visibleString; // PUBLIC variables can be seen outside the class, including in the Unity Inspector.
    [HideInInspector] public int invisibleInt; // [HideInInspector] hides public variables from the inspector.
    [SerializeField]private float visibleFloat; // [SerializeField] makes private variables visible in the inspector.
    
    public static int globalInt; // STATIC variables are shared by all instances of the same class.
    #endregion
    #endregion
    #region Functions
    // FUNCTIONS need an OUTPUT type, a NAME, and INPUTS.
    float AddThenDivide(float firstNum, float secondNum, float divisor) 
    {                                 // Everything within the BRACKETS {} after a function will run in sequential order when the function is run unless otherwise stated.
        float added;                  // Variables declared inside of functions only exist until the function ends.
        added = firstNum + secondNum; // The value on the right of EQUALS changes the value on the left.

        return added / divisor;       // Use RETURN to declare the value output by the function.
    }

    // VOID functions don't return an output.
    void DoNumberStuff()
    {
        anInt = 1;                                       // You can reference any variables visible to the function.
        aFloat = AddThenDivide(1, 2, aFloat);            // You can call functions within a function.
        CheckIntValue(0);                                // Void functions don't use an equals sign.
        DoNumberStuff();                                 // A function can even be called within itself.
        babyScript.aFloat = AddThenDivide(aFloat, 2, 3); // You can reference variables of other classes with a dot.
                                                         // Void functions don't need returns.
    }
    public void CheckIntValue (int i) // Functions can be public or private as well.
    {
        int j = 1;
        if (i == 10)                  // IF STATEMENTS look for a bool value (usually whether something is EQUAL (==) or NOT equal (!=)
        {                             // Everything within the brackets will run if it meets the conditions of the if statement.
            j = i + 1;
            j = j / 2;
            return; // Adding a return partway through your function can help it end early on certain conditions.
        }
        else if (i == -1) // If you only have one line in an if/else statement, you don't need brackets.
            j = i + 1;
        else
        {
            aString = "Add some text to the string.";
        }
        for (j = 0; j < i; j++) // FOR LOOPS let you set initial values, and end checks and runs until it meets those values. 
        {
            aString = "" + j;
        }
        while (j < i) // WHILE LOOPS let you loop until a value is set.
        {
            j++;
        }
    }
    #endregion
    #region Syntax & Errors
    // SYNTAX is a specific way of writing code that ensures it works.
    public void DoThis()    // Functions need parenthesis (), even if with no variables, that's the function syntax.
    {                       // Encapsulating things like functions, need curly brackets {}.
        anInt++;            // All lines need to end with a semicolon ; so Unity can read it as a separate line.
        aString = aList[1]; // Referencing an item on a list requires square brackets [].
    }
    // If you mess up syntax, you will get a red squiggly line under the error.
    // You can also get a red squiggly line if you reference something that doesn't exist.
    // The game will fail to compile until you fix these errors. (You can't even play it in test mode.)
    // Some errors will not show until the game runs. Often this is because you forgot to set a variable before trying to reference it.
    #endregion
    #region Special Unity Features
    void Start()             // Called once as the scene starts or the object is created.
    {
        Vector3 logPos;                    // VECTOR3 provides x/y/z coordinates for position, scale, speed, etc.
        Quaternion logRotation;            // QUATERNION provides functions for 3D rotation.

        logPos = transform.position;       // TRANSFORM allows you to look up and change your object's POSITION, ROTATION, and SCALE.
        logRotation = Quaternion.identity; // Quaternion and Vector3 have static variables for basics, like (0,0,0), (1,1,1), unrotated... etc.
        gameObject.SetActive(false);       // You can reference the GAMEOBJECT variable to turn it on and off.
        Destroy(gameObject);               // You can also completely remove something from the game.
    }
    private void OnEnable()  // Runs right after each time an object is enabled.
    {
        
    }
    private void OnDisable() // Runs right before each time an object is disabled.
    {
        
    }
    private void OnDestroy() // Runs right before an object is destroyed.
    {
       
    }
    void Update()            // Called once per frame
    {
        
    }
    private void OnCollisionEnter(Collision collision) // Called when this object hits another object, including data about that object and the collision.
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)        // Called when this object passes through a trigger object.
    {
        
    }
    private void OnTriggerExit(Collider other) 
    { 
    }
    #endregion

}
