using UnityEngine;

/// <summary>
/// GameAction -- When activated, changes the color and intensity of the light to the indicated values.
/// </summary>
public class ChangeLighting : GameAction
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "When activated, changes the color and intensity of the light to the indicated values.";
    #endregion

    #region Variables
    [Tooltip("The color to change the light to.")]public Color color = Color.white;
    [Tooltip("The color will be multiplied by this number for intensity.")]public float intensity = 1;
    #endregion

    public override void Activate()
    {
        LightingManager.SetLight(color, intensity);
    }
    public override void Deactivate()
    {
        LightingManager.ResetLight();
    }
}
