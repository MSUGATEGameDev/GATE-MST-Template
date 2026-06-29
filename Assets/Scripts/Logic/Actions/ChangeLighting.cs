using UnityEngine;

public class ChangeLighting : GameAction
{
    public Color color = Color.white;
    public float intensity = 1;
    public override void Activate()
    {
        LightingManager.SetLight(color, intensity);
    }

    public override void Deactivate()
    {
        LightingManager.ResetLight();
    }
}
