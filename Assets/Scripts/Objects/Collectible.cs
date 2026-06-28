using UnityEngine;

public class Collectible : GameTrigger
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameTrigger --\n" +
        "Object that can be picked up by the player. Can also trigger an action.";
    [Header("Collectible Settings")]
    public string collectibleName = "";
    [Tooltip("Announce to the player when they've collected the object.")]public bool announceCollection = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollection();
        }   
    }
    public virtual void OnCollection() 
    {
        if (announceCollection)
        {
            HUD.DisplayAnnouncement(collectibleName + " Collected!");
        }
        ActivateItems();
        gameObject.SetActive(false);
    }
}
