using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Object -- Displays text only when the player is present.
/// </summary>
public class Tip : MonoBehaviour
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Object --\n" +
        "Displays text only when the player is present.";
    #endregion

    #region Inspector-Editable Variables
    [Tooltip("The message to display.")]public GameObject tip;
    [Tooltip("How many times it will display the message before not doing it anymore.\n -1 for infinite.")]public int maxAppearances = 3;
    [Tooltip("If checked, all tips of the exact same spelling will be grouped into the same max appearances. With the biggest max being the one claimed")] public bool sharedMax;
    #endregion

    #region Static Variables
    static Dictionary<string, (int, int)> tipsAndCounts = new();
    #endregion

    #region Internal Varaiables
    int appearances = 0;
    string txt;
    #endregion

    private void Awake()
    {
        txt = tip.GetComponent<TextMeshPro>().text;
        tip.SetActive(false);
        if (tipsAndCounts.ContainsKey(txt))
        {
            if (tipsAndCounts[txt].Item1 < 0 || maxAppearances < 0)
            {
                tipsAndCounts[txt] = (-1, 0);
            }
            else if (tipsAndCounts[txt].Item1 < maxAppearances)
            {
                tipsAndCounts[txt]= (maxAppearances,0);
            }
        }
        else
        {
            tipsAndCounts.Add(txt, (maxAppearances,0));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sharedMax)
            {
                if (tipsAndCounts[txt].Item2 < tipsAndCounts[txt].Item1 || tipsAndCounts[txt].Item1 < 0)
                {
                    tipsAndCounts[txt] = (tipsAndCounts[txt].Item1,tipsAndCounts[txt].Item2+1);
                    tip.SetActive(true);
                }
            }
            else
            {
                if (appearances < maxAppearances || maxAppearances < 0)
                {
                    appearances++;
                    tip.SetActive(true);
                }
            }
            
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tip.SetActive(false);
        }
    }
}
