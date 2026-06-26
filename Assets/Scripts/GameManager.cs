using UnityEngine;

public class GameManager : MonoBehaviour
{
    // A spot for general game controlly things.
    public GameObject ceiling;
    public bool indoors = true;

    private void Awake()
    {
        if(indoors)
            ceiling.SetActive(true);
    }
}
