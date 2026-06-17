using UnityEngine;

public class DisplayText : GameAction
{
    public enum TextDisplayType { InGame, OnScreen}
    [SerializeField]TextDisplayType textDisplayType;

    public override void Activate()
    {
        if (textDisplayType == TextDisplayType.InGame)
        {

        }
        else
        {

        }
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
