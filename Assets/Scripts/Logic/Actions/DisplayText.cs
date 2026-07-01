using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// -- GameAction -- Displays text to the player at its location or at the bottom of the screen.
/// </summary>
public class DisplayText : GameAction
{
    #region Description For Unity Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Displays text to the player at its location or at the bottom of the screen.";
    #endregion

    #region Inspector-Editable Variables
    public enum TextDisplayType { InGame, OnScreen }
    
    [Header("Settings")]
    [Tooltip("In Game - Appear wherever you've placed this object.\n" +
        "On Screen - Display at the bottom of the screen.")]                public TextDisplayType textDisplayType;
    [Tooltip("What should this say?")]                                      public string textToDisplay="";
    [Tooltip("Set to the default message duration for the project.")]       public bool defaultDuration = true;
    [Tooltip("Amount of time for text to display (in seconds).\n" +
        "Negative values indicate indefinite display.\n" +
        "(Indefinite applies to in-game only.)")]                           public int durationIfNotDefault = 5;
    [Tooltip("On Deactivate - Hide the text.\n" +
        "(In-Game Only)")]                                                  public bool deactivateDisablesDisplay = false;
    [Tooltip("Display text immediately even if other text is present.\n" +
        "(On-Screen Only)")]                                                public bool priorityMessage = false;
    #endregion

    #region Internal Variables
    TextMeshPro inGameDisplay;
    Coroutine displayTimer;
    #endregion

    private void Start() // The last function run before the object first appears in the game.
    {
        inGameDisplay = GetComponentInChildren<TextMeshPro>(); //
    }

    public override void Activate() // When activated as an Action.
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
    public override void Deactivate() // When deactivated as an Action.
    {
        if (deactivateDisablesDisplay && textDisplayType == TextDisplayType.InGame)
        {
            if (displayTimer != null)
            {
                StopCoroutine(displayTimer);
            }
            inGameDisplay.text = "";
        }
    }
    IEnumerator DisplayLocal() // Display at the location for the indicated duration of time.
    {
        if (defaultDuration)
            yield return new WaitForSeconds(HUD.singleton.defaultNoticeDuration);
        else
            yield return new WaitForSeconds(durationIfNotDefault);
        inGameDisplay.text = "";
    }

}
