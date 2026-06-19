using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayText : GameAction
{
    [Header("Settings")]
    public TextDisplayType textDisplayType;
    [ReadOnly]
    [TextArea(3,6)]
    public string about = "This is where I would put what this is about.";
    [Tooltip("In game will appear wherever you've placed this object. On Screen will be displayed at the bottom of the screen.")]public enum TextDisplayType { InGame, OnScreen }
    public string textToDisplay="";
    [Tooltip("Set to the default message duration for the project.")]public bool defaultDuration = true;
    [Tooltip("Amount of time for text to display (in seconds). Negative values indicate indefinite display. (Indefinite applies to in-game only.)")]public int durationIfNotDefault = 5;
    [Tooltip("If this receives a deactivate signal, it will hide the text (applies to in-game only)")]public bool deactivateDisablesDisplay = false;
    [Tooltip("Will pause whatever is currently in the notice area to display this message (on-screen only)")] public bool priorityMessage = false;
    TextMeshPro inGameDisplay;
    Coroutine displayTimer;
    private void Start()
    {
        inGameDisplay = GetComponentInChildren<TextMeshPro>();
    }
    public override void Activate()
    {
        if (textDisplayType == TextDisplayType.InGame)
        {
            inGameDisplay.text = textToDisplay;
            if(displayTimer!= null)
            {
                StopCoroutine(displayTimer);
            }
            displayTimer = StartCoroutine(DisplayLocal());
        }
        else
        {
            if (defaultDuration)
                HUD.DisplayNotice(textToDisplay,priorityMessage);
            else
                HUD.DisplayNotice(textToDisplay, priorityMessage, durationIfNotDefault);
        }
    }
    IEnumerator DisplayLocal()
    {
        if (defaultDuration)
            yield return new WaitForSeconds(HUD.current.defaultNoticeDuration);
        else
            yield return new WaitForSeconds(durationIfNotDefault);
        inGameDisplay.text = "";
    }
    public override void Deactivate()
    {
        if (deactivateDisablesDisplay && textDisplayType==TextDisplayType.InGame)
        {
            if (displayTimer != null)
            {
                StopCoroutine(displayTimer);
            }
            inGameDisplay.text = "";
        }
    }
}
