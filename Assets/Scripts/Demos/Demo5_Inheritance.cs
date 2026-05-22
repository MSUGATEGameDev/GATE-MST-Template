using UnityEngine;
                                                // Adding a colon (:) after the name of the class followed by another class name classifies it as an IHERITED class.
                                                // Inherited classes make use of all the variables and functions of the original class, while adding their own.
public class Demo5_Inheritance : MonoBehaviour  // This class is inherited from MonoBehavior, Unity's class that attaches to game objects.
{                                               // This grants us access to position and rotation data as well as allows our variables to appear in the inspector.

    private Vector3 startingPos;                        // Private variables will not show up in the inspector.
    public float health = 100;                          // Public variables will show up in the inspector.

    [HideInInspector] public bool isPoisoned = false;   // [HideInInspector] hides public variables from the inspector.
                                                        // isPoisoned could be affected by poisonous objects on contact, but doesn't need to be edited when building the game.
    [SerializeField] private string playerName;   // [SerializeField] shows private variables in the inspector.

    private void Start() // Code here will run as soon as the object appears for the first time.
    {
        startingPos = transform.position;   // transform allows you to manipulate the game object's position and rotation.
                                            // This makes note of the position at the start of the game.
    }
    private void OnEnable() // Code here will run whenever (and as often as) the object is un-hidden.
    {
        transform.position = startingPos;   // This will move it back to the initial position whenever it is re-enabled.
        health = 100;                       // This will reset health whenever re-enabled.
    }
    private void Update() // Code here will run every frame.
    {
        if (isPoisoned)
        {
            health -= 2 * Time.deltaTime;   // Time.deltaTime is the amount of time (in seconds) since the last frame.
                                            // Multiplying by delta time allows things to happen at the same rate even as framerate fluctuates.
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);    // You can turn off and on an object in the game by setting its game object active or inactive.
        }
    }

    Door favoriteDoor; // You can drag in-game objects with different MonoBehaviour-enabled classes attached to them to the variable field to interact with them.

    GameObject bulletPrefab; // You can drag a prefab to a GameObject variable to be able to spawn it later in the game.
    void FireBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab); // Instantiate makes a new object based on the prefab and places it in the world.
    }
}
