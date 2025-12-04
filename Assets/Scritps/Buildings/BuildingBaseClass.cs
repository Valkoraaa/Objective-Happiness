using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBaseClass : MonoBehaviour
{
    // At the end of the day, the value brings more ressources depending on the building type
    public int peopleWorking = 0; // Increments when people going to job every morning and decriments if people are quitting.
    protected GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    void OnEnable() { EventManager.EndOfDay += Evening; }
    void OnDisable() { EventManager.EndOfDay -= Evening; }

    void Evening()
    {
        // Calculates the profit at the end of the day based on people who were present and sends to GameManager.cs
        // Calculating profit, reducing people working here
    }
}