using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Attach this class to something to have it always rotate to look at the player's head, such that a 2D visual element is always visible from any angle.

    public bool invert = true;

    void Update()
    {
        transform.LookAt(Player.singleton.playerCamTransform);
        if (invert)
        {
            transform.eulerAngles = new Vector3(
                -transform.eulerAngles.x, 
                transform.eulerAngles.y + 180f, 
                transform.eulerAngles.z
            );   
        }
    }
}
