using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField]Light light1;
    [SerializeField]Light light2;
    static Color defaultColor;
    static float defaultIntensity;

    #region Singleton
    public static LightingManager current;
    private void Awake()
    {
        if(current == null)
        {
            current = this;
            defaultColor = light1.color;
            defaultIntensity = light1.intensity;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public static void SetLight(Color color)
    {
        SetLight(color, 1);
    }
    public static void SetLight(Color color, float intensity)
    {
        current.light1.color = color;
        current.light2.color = color;
        current.light1.intensity = intensity;
        current.light2.intensity = intensity;
    }
    public static void ResetLight()
    {
        current.light1.color = defaultColor;
        current.light2.color = defaultColor;
        current.light1.intensity = defaultIntensity;
        current.light2.intensity = defaultIntensity;
    }
}
