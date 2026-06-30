using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// -- Game System -- Makes it easy for other classes to reference standard colors and materials programmatically.
/// </summary>
public class ColorManager : MonoBehaviour
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Game System --\n" +
        "Makes it easy for other classes to reference standard colors programmatically.";
    #endregion

    #region Instantiating as a Singleton
    // Singletons are where only one instance of this class can exist at one time in the game.
    // This allows other classes to reference it easily.
    public static ColorManager singleton;
    private void Awake() // The very first function run by a class the instant it is created.
    {
        if (singleton != null)
            Destroy(this);
        singleton = this;
    }
    #endregion

    #region Inspector-Editable Variables
    public enum StandardColor { Red, Orange, Yellow, Green, Blue, Violet };

    [Header("Material References")]
    [Tooltip("Solid Materials made of the 6 Primary and secondary colors.")]public List<Material> standardMattes;
    [Tooltip("Transparent Materials made of the 6 Primary and secondary colors.")] public List<Material> standardHolos;
    [Tooltip("Transparent Black material.")] public Material holoBlack;
    #endregion

}
