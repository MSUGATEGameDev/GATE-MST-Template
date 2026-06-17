using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<bool> collectedKeys = new() { false, false, false, false, false, false };
    public enum CollecibleKey { Red, Orange, Yellow, Green, Blue, Violet };

}
