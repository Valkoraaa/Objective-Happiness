using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Character : MonoBehaviour {
	public BuildingBaseClass job;
	public GameObject school;
	public GameObject home;
	[SerializeField] private GameManager gm;

	[SerializeField] private int foodAmount = 1; // The needed amount of food to survive

	public bool isTired = false;

    public void Init(GameObject actualJob, GameObject actualHome)
    {
        job = actualJob.GetComponent<BuildingBaseClass>(); // Appliquons le script
        home = actualHome;
    }

    private void Start()
    {
        gm = GameManager.Instance;
    }

    // Cycles through his day: goes to job, exhausts
    public void CycleDay()
    {
        // If isn't tired -> goes to school or to job

        if (home == null) // Looses his job if he doesn't have a house
        {
            isTired = true;
            gm.prosperity -= 1;
        }
        else GoHome(); //va a sa maison


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
        transform.Translate(new Vector3(job.transform.position.x, job.transform.position.y, job.transform.position.z));
        job.peopleWorking++; // Arrived at work
        // Work for some time
    }
    public void GoHome()
    {
        transform.Translate(new Vector3(home.transform.position.x, home.transform.position.y, home.transform.position.z));
        job.peopleWorking++; // Quit his work
    }
}