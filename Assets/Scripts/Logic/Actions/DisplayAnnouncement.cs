using UnityEngine;

public class DisplayAnnouncement : GameAction
{
    public string titleText = "";
    public string subtitleText = "";
    [Tooltip("Pauses any ongoing announcements to display this.")] public bool priority = false;
    [Tooltip("Lasts for the default duration for the game.")] public bool defaultDuration = true;
    [Tooltip("Duration of message (in seconds)")] public int durationIfNotDefault = 5;

    public override void Activate()
    {
        if (defaultDuration)
        {
            HUD.DisplayAnnouncement(titleText, subtitleText, priority);
        }
        else
        {
            HUD.DisplayAnnouncement(titleText, subtitleText, priority, durationIfNotDefault);
        }
    }

    public override void Deactivate()
    {
        
    }
}
