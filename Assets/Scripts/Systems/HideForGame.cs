using UnityEngine;

public class HideForGame:MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
