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
    
    public bool resetOnActivate = false;

    public enum TimerDisplayOptions {Disabled,InGame,OnScreen}
    [Tooltip("Should a countdown be displayed? If so, should it be displayed in a corner of the screen, or on the timer?")]
    public TimerDisplayOptions displayCountdown = TimerDisplayOptions.Disabled;
    [Tooltip("How long will the timer wait after activation to activate the objects assigned to it?")]
    public float timeToStart;
    [Tooltip("After activating, how long will the timer take to activate again (if repeating)")]
    public float timeToRepeat;
    #endregion

    #region Components
    [Header("Visual Components")]
    TextMeshPro timerDisplay;
    Renderer placeholder;
    #endregion

    private void Start()
    {
        placeholder= GetComponent<Renderer>();
        timerDisplay = GetComponentInChildren<TextMeshPro>();
    }

    Coroutine countdown;
    IEnumerator silentCountdown()
    {
        yield return null;
    }
    IEnumerator visualCountdown()
    {
        yield return null;
    }
}
