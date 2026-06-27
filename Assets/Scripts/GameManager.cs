using UnityEngine;

/// <summary>
/// Game System -- Manages some general game features that don't have dedicated managers.
/// </summary>
public class GameManager : MonoBehaviour
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Game System --\n" +
        "Manages some general game features that don't have dedicated managers.";

    [Tooltip("The in-game ceiling. Hidden in editor to allow editing.")]public GameObject ceiling;
    public bool indoors = true;

    private void Awake()
    {
        if(indoors)
            ceiling.SetActive(true);
    }
}
