using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Disables a game object if the game is running on a particular platform.
/// </summary>
public class HideForPlatforms : MonoBehaviour
{
    
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Tool --\n" +
        "Disables a game object if the game is running on a particular platform.";
    #endregion

    [Tooltip("This object will not appear if run from any of the platforms on this list.")]public List<RuntimePlatform> hideOnPlatforms;

    private void Start()
    {
        foreach (var platform in hideOnPlatforms)
        {
            if (Application.platform == platform)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
