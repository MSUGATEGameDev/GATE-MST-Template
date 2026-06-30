using UnityEngine;

public class CollectibleKey : Collectible
{
    [Header("Key Settings")]
    [Tooltip("Color code the key to allow it to work with game logic.\nIf name is left blank, color will be used for the name.")]public ColorManager.StandardColor keyColor;
    private void Start()
    {
        if (collectibleName == "")
        {
            collectibleName = keyColor.ToString() + " Key";
        }
        GetComponentInChildren<Renderer>().material = ColorManager.singleton.standardMattes[(int)keyColor];
    }
    public override void OnCollection()
    {
        base.OnCollection();
        ObjectivesManager.KeyCollected(keyColor);
    }
}
