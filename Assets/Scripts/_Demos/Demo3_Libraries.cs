using System.Collections.Generic;
using UnityEngine;

public class Demo3_Libraries
{
    // You don't need to write everything. Lots of people have written code and made it available.
    void MathStuff()
    {
        float testVar = Mathf.Sqrt(25);         // Mathf has a bunch of math functions.
        int randomVar = Random.Range(1, 700);   // Random can generate random numbers.
    }

    void SpatialStuff()
    {
        Vector3 positionData = new Vector3(12.4f, 100, 90);                 // Vector3 stores xyz values, great for 3D position coordinates.
        Vector3 destination = new Vector3(1, 100, 1);
        float distanceToDest = Vector3.Distance(positionData, destination); // Vector3 can tell us the distance between 2 points (and a lot more).
                                                                            // Vector2 also exists for 2D coordinates.
             

        Quaternion rotationData = Quaternion.Euler(120, 60, 31.2f); // Quaterion stores rotation data.
        Quaternion newRotation = Quaternion.Euler(1,100,5);
        float angleToNew = Quaternion.Angle(rotationData,newRotation); // Quaternion can measure angles (and a lot more).
    }

    void ListsAndStuff()
    {
        List<string> wordList = new();              // LISTS allow you to store many of the same type of value.
                                                    // new() creates an empty instance of a list or other complex variable.
        wordList.Add("Funny");                      // .Add() adds an item to the list.
        wordList.Add("Welded");
        wordList.Add("Test");
        
        string testString = wordList[1];            // BRACKETS ([]) lookup the value in a certain position (index). (First entry is 0)
        wordList.Remove("Funny");                   // REMOVE looks for a value and removes from the list.
        wordList.RemoveAt(1);                       // REMOVEAT removes the value in a certain index.
        wordList[1] = "New Word";                   // You can update a value at a certain index using brackets as well.

        foreach (string word in wordList)           // You can loop through all variables in a list with foreach.
        { 
            Debug.Log(word);                        // Outputs each word in the list into the console one at a time.       
        }


        Dictionary<string,int> scores = new();      // Dictionaries store data that can be looked up by a key.
        scores.Add("DeathKiller", 1);
        scores.Add("WaveHunter", 15);
        scores.Add("Paulina", 6);

        int paulinasScore = scores["Paulina"];      // Brackets on a dictionary lookup by key instead of index.
        scores["WaveHunter"] = 16;                  // You can update a value at a certain key as well.
    }
}
