using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Character : MonoBehaviour {
	public BuildingBaseClass job;
	public BuildingBaseClass school;
	public House house;
	private GameManager gm;

	[SerializeField] private int foodAmount = 1; // The needed amount of food to survive

    public bool isTired = false;

    // Initialises game character by passing the game objects and accessing their scripts
    public void Init() // Newborn function which
    {
        job = null;
        house = null;
        school = null;
        gm = GameManager.Instance; // Reference to the game manager
    }
    public void Init(GameObject actualJob, GameObject actualHouse)
    {
        job = actualJob.GetComponent<BuildingBaseClass>(); // Applying scripts
        house = actualHouse.GetComponent<House>();
        gm = GameManager.Instance; // Reference to the game manager
    }

    // Cycles through his day: goes to job, exhausts
    public void CycleDay()
    {
        // If isn't tired -> goes to school or to job

        if (house == null) // Doesn't job if he doesn't have a house
        {
            isTired = true;
            gm.prosperity -= 1;

            // Seek for house
        }
        else GoHouse(); //va a sa maison

        if (gm.food > 0)
            gm.food -= foodAmount;
        else
            Die();

        if (!isTired) GoWork();
    }

	public void Die() {
		// Character dies
		// TODO: plays animation and despawns
        gm.prosperity -= 1;
		Destroy(gameObject); // Self destruction
    }

    public void GoWork()
    {
        transform.Translate(job.transform.position);
        job.peopleWorking++; // Arrived at work
        // Work for some time
    }
    public void GoHouse()
    {
        transform.Translate(house.transform.position);
        job.peopleWorking--; // Quit his work
    }
}