using UnityEngine;

public class CollectibleKey : Collectible
{
    public ObjectivesManager.KeyColor keyColor;
    public override void OnCollection()
    {
        base.OnCollection();
        ObjectivesManager.KeyCollected(keyColor);
    }
}
