using UnityEngine;

/// <summary>
/// Game System - Interfaces all classes with the in-game lighting.
/// </summary>
public class LightingManager : MonoBehaviour
{
    #region Description for Inspector.
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Game System --\n" +
        "Interfaces all classes with the in-game lighting.";
    #endregion

    #region Inspector-Editable Variables
    [SerializeField]Light light1;
    [SerializeField]Light light2;
    #endregion
    
    #region Singleton & Defaults
    // A singleton is where only one of this class can exist at any given time.
    public static LightingManager singleton;
    static Color defaultColor;
    static float defaultIntensity;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            defaultColor = light1.color;
            defaultIntensity = light1.intensity;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    #region Functions

    /// <summary>
    /// Sets the overall lighting of the game to the indicated color with an intensity of 1.
    /// </summary>
    /// <param name="color">The color to set the lights to.</param>
    public static void SetLight(Color color)
    {
        SetLight(color, 1);
    }
    /// <summary>
    /// Sets the overall lighting of hte game to the indicated color and intensity.
    /// </summary>
    /// <param name="color">The color to set the lights to.</param>
    /// <param name="intensity">The intensity to set the lights to.</param>
    public static void SetLight(Color color, float intensity)
    {
        singleton.light1.color = color;
        singleton.light2.color = color;
        singleton.light1.intensity = intensity;
        singleton.light2.intensity = intensity;
    }
    /// <summary>
    /// Resets the lights to their original value set in the inspector.
    /// </summary>
    public static void ResetLight()
    {
        singleton.light1.color = defaultColor;
        singleton.light2.color = defaultColor;
        singleton.light1.intensity = defaultIntensity;
        singleton.light2.intensity = defaultIntensity;
    }
    #endregion
}
