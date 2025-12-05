using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobBaseClass : MonoBehaviour
{
    // At the end of the day, the value brings more ressources depending on the building type
    protected GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    protected int gainPerPerson = 2; // How much this particular job produces

    public void Work()
    {
        // Each character that works on this job, calls this function and produce a gain
        // So, the EventManager.cs evening controlls characters and they control the jobs
    }
}