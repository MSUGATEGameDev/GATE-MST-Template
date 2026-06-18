using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ColorManager : MonoBehaviour
{
    public static ColorManager current;
    public enum StandardColor { Red, Orange, Yellow, Green, Blue, Violet };
    public List<Material> standardMattes;
    public List<Material> standardHolos;

    private void Awake()
    {
        if (current != null)
            Destroy(this);
        current = this;
    }
}
