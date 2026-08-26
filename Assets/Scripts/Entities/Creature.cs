using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Creature : MonoBehaviour
{
    // Variables
    public bool isFriendly = true;          // Bool is a true/false value.
    public string creatureName = "Alphie";  // String is text.
                                            // Some variable names are already taken by the system and you need to make things a little more unique.
    int limbs = 4;                          // Int is a whole number
    float health = 90.2f;                   // Float is a decimal number
    float maxHealth = 100f;

    public TextMeshPro nameTag;             // You can reference other monobehaviours

    List<string> favoriteFoods = new() { "Kibble", "Steak", "Duck" };
    Dictionary<string, bool> friendlyHumans = new() { { "Jim", true }, {"Paula", true },{"Leonard", false } };

    void Start() // Start is called first thing and once.
    {
        nameTag.text = creatureName;
    }

    void Update() // Update is called once per frame
    {
        if(health < maxHealth)
        {
            health += 2 * Time.deltaTime; // Time.deltaTime is the amount of time (in seconds) since the last frame, so 2*Time.deltaTime makes the number steadily increase each frame at a rate of 2 per second.
        }
    }
    public void BePet(string petter)
    {
        if (!friendlyHumans.ContainsKey(petter))
        {
            friendlyHumans.Add(petter, true);
        }
    }
    public void BeHit (string hitter, float strength)
    {
        health -= strength;
        if (friendlyHumans.ContainsKey(hitter))
        {
            friendlyHumans[hitter] = false;
        }
        else
        {
            friendlyHumans.Add(hitter, false);
        }
    }
    public string SayFoodILike()
    {
        int foodIndex = Random.Range(0, 2);
        return favoriteFoods[foodIndex];
    }
}
