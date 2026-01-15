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
    public float timeToStart;
    [Tooltip("After activating, how long will the timer take to activate again (if repeating)")]
    public float timeToRepeat;
    Coroutine timerCoroutine;
    #endregion

    #region Components
    [Header("Visual Components")]
    TextMeshPro timerDisplay;
    #endregion

    private void Start()
    {
        timerDisplay = GetComponentInChildren<TextMeshPro>();
        if (activeAtStart)
        {
            switch (displayCountdown)
            {
                case TimerDisplayOptions.Disabled:
                    timerCoroutine = StartCoroutine(silentCountdown());
                    break;
                case TimerDisplayOptions.InGame:
                    break;
                default:
                    break;
            }
        }
    }

    Coroutine countdown;
    IEnumerator silentCountdown()
    {
        yield return new WaitForSeconds(timeToStart);
        Activate();
        if (repeats && resetOnActivate)
        {
            while (isActiveAndEnabled)
            {
                yield return new WaitForSeconds(timeToRepeat);
                Activate();
            }
        }
        
    }
    IEnumerator visualCountdown()
    {
        yield return null;
    }
}
