using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Game System -- Manages some general game features that don't have dedicated managers.
/// </summary>
public class GameManager : MonoBehaviour
{
    // This is a very incomplete class. :D
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Game System --\n" +
        "Manages some general game features that don't have dedicated managers.";

    [Tooltip("The in-game ceiling. Hidden in editor to allow editing.")]public GameObject ceiling;
    [Tooltip("If enabled, the in-game ceiling will appear at the start.")]public bool indoors = true;
    [Tooltip("GameActions you would like to activate when the game starts.")] public List<GameAction> actions = new();
    private void Awake()
    {
        if(indoors)
            ceiling.SetActive(true);
    }
    private void Start()
    {
        foreach(GameAction action in actions)
        {
            if(action != null)
            {
                action.Activate();
            }
        }
    }
}
