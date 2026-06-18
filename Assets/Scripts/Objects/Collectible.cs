using UnityEngine;

public class Collectible : GameTrigger
{
    [Header("Collectible Settings")]
    public string collectibleName = "";
    [Tooltip("Announce to the player when they've collected the object.")]public bool announceCollection = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (announceCollection)
            {
                HUD.DisplayAnnouncement(collectibleName + " Collected!");
            }
            OnCollection();
        }
        
    }
    public virtual void OnCollection() 
    { 
        ActivateItems();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
