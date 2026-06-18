using UnityEngine;

public class Collectible : GameTrigger
{
    public string objectName = "";
    public bool announceCollection = true;
    private void OnTriggerEnter(Collider other)
    {
        
        OnCollection();
        if (announceCollection)
        {
            HUD.DisplayAnnouncement(objectName + " Collected!");
        }
    }
    public virtual void OnCollection() 
    { 
        ActivateItems(); 
    }
}
