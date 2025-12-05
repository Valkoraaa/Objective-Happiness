using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Character : MonoBehaviour {
	[Header("Character occupations")]
	public JobBaseClass job;
	public JobBaseClass school;
    public House house;

	private GameManager gm;

	[SerializeField] private int foodAmount = 1; // The needed amount of food to survive

    public bool isTired = false;

    // Initialises game character by passing the game objects and accessing their scripts
    public void Init() // Newborn function, doesn't take any arguments
    {
        job = null;
        house = null;
        school = null;
        gm = GameManager.Instance; // Reference to the game manager
    }
    public void Init(GameObject actualJob, GameObject actualHouse) // Receives game objects as arguments, then gets their scripts
    {
        job = actualJob.GetComponent<JobBaseClass>(); // Applying scripts
        house = actualHouse.GetComponent<House>();
        school = null;
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
        }
        else GoHouse(); //va a sa maison

        if (gm.food > 0) gm.food -= foodAmount;
        else Die();

        if (!isTired) GoWork();
    }

	public void Die()
    {
		// Character dies
		// TODO: plays animation and despawns
        gm.prosperity -= 1;
		Destroy(gameObject); // Self destruction
    }

    public void GoWork()
    {
        transform.Translate(job.transform.position);
        job.Work();
        // Work for some time

        isTired = true; // Gets tired after work
    }
    public void GoHouse()
    {
        transform.Translate(house.transform.position); // Goes home
        // Sleeps for some time
        gm.food -= foodAmount; // Eats
        isTired = false; // Rests and gets energized
    }
}