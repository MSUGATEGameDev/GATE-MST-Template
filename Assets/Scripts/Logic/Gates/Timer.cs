using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : GameTrigger
{   // A trigger that delays the activation of the objects assigned to it.
    #region Settings
    [Header("Settings")]
    [Tooltip("Does the timer start without activation when the scene loads?")]
    public bool activeAtStart = false;
    
    [Tooltip("Does the timer end after one activation?")]
    public bool repeats = false;

    [Tooltip("Does the timer start back up as soon as its finished?")]
    public bool resetOnActivate = false;

    public enum TimerDisplayOptions {Disabled,InGame,OnScreen}
    [Tooltip("Should a countdown be displayed? If so, should it be displayed in a corner of the screen, or on the timer?")]
    public TimerDisplayOptions displayCountdown = TimerDisplayOptions.Disabled;
    [Tooltip("How long will the timer wait after activation to activate the objects assigned to it?")]
    public int timeToStart;
    [Tooltip("After activating, how long will the timer take to activate again (if repeating)")]
    public int timeToRepeat;
    #endregion

    #region Components
    [Header("Visual Components")]
    [SerializeField]TextMeshPro timerDisplay;
    Coroutine timerCoroutine;
    #endregion

    private void Start() // This code runs right when the game starts.
    {
        timerDisplay = GetComponentInChildren<TextMeshPro>(); // Find the timer display.
        if (activeAtStart) Activate(); // Start the timer if it's set to do so. 
        if(displayCountdown == TimerDisplayOptions.InGame) GetComponent<LookAtPlayer>().enabled = true; // Enable the component to look at player if selected.
    }
    public override void Activate() // Activating a timer starts the countdown.
    {
        if (timerCoroutine == null) // If the timer is already running, the timer doesn't need to be started.
        {
            if (displayCountdown == TimerDisplayOptions.Disabled)
                timerCoroutine = StartCoroutine(SilentCountdown());
            else
                timerCoroutine = StartCoroutine(VisualCountdown());
        }

    }
    public override void Deactivate() // Deactivating a timer stops it from going, it doesn't deactivate its children.
    {
        StopCoroutine(timerCoroutine);
    }
    Coroutine countdown;
    IEnumerator SilentCountdown() // This just waits the appropriate amount of time and starts the timer.
    {
        yield return new WaitForSeconds(timeToStart);
        ActivateItems();
        if (repeats && resetOnActivate)
        {
            while (isActiveAndEnabled)
            {
                yield return new WaitForSeconds(timeToRepeat);
                ActivateItems();
            }
        } 
    }
    IEnumerator VisualCountdown() // This one will display a number countdown every second (either in the determined spot in-game or on the screen)
    {
        int runTime = timeToStart;
        while (runTime > 0)
        {
            if (displayCountdown == TimerDisplayOptions.InGame)
                timerDisplay.text = "" + runTime;
            else
            {
                PlayerController.singleton.DisplayOnScreenText("" + runTime);
            }
            runTime--;
            yield return new WaitForSeconds(1);
        }
        ActivateItems();
        if (repeats && resetOnActivate)
        {
            while (isActiveAndEnabled)
            {
                runTime = timeToRepeat;
                while (runTime > 0)
                {
                    if (displayCountdown == TimerDisplayOptions.InGame)
                        timerDisplay.text = "" + runTime;
                    else
                    {
                        PlayerController.singleton.DisplayOnScreenText("" + runTime);
                    }
                    runTime--;
                    yield return new WaitForSeconds(1);
                }
                ActivateItems();
            }
        }
    }
}
