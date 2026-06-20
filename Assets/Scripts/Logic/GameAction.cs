using UnityEngine;

public abstract class GameAction : MonoBehaviour
{
    
    // A class made to create a universal point of activation for all game objects that can be activated or deactivated.
    // If a script is made to inherit this class, it will work with any trigger looking for an "GameAction".

    // This command can be called from a trigger to activate the object.
    public abstract void Activate();
    
    // This command can be called from a trigger to deactivate the object.
    public abstract void Deactivate();
}
