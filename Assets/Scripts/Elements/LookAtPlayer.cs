using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Attach this class to something to have it always rotate to look at the player's head, such that a 2D visual element is always visible from any angle.
    void Update()
    {
        transform.LookAt(PlayerController.singleton.playerHead);
    }
}
