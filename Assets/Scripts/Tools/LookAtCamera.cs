using UnityEngine;
/// <summary>
/// -- Tool -- Attach this class to something to have it always rotate to look at the camera, such that a 2D visual element is always visible from any angle.
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Tool --\n" +
        "Attach this class to something to have it always rotate to look at the camera, such that a 2D visual element is always visible from any angle.";
    #endregion

    #region Inspector-Editable Variables
    [Tooltip("Flip the object around if its facing the wrong way.")] public bool invert = true;
    #endregion

    void Update() // Run every frame.
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
