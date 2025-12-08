using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour {
    /*public BuildingBaseClass job;
	public BuildingBaseClass school;
	public House house;*/
    public GameObject job;
    public GameObject house;
	private GameManager gm;
    public bool hasSkin = false;
    private float travelDuration = 1;

	[SerializeField] private int foodAmount = 1; // The needed amount of food to survive

    public bool isTired = false;

    // Initialises game character by passing the game objects and accessing their scripts
    public void Init() // Newborn function which
    {
        job = null;
        house = null;
        //school = null;
        gm = GameManager.Instance; // Reference to the game manager
    }
    public void Init(GameObject actualJob, GameObject actualHouse)
    {
        //job = actualJob.GetComponent<BuildingBaseClass>(); // Applying scripts
        //house = actualHouse.GetComponent<House>();
        job = actualJob;
        house = actualHouse;
        gm = GameManager.Instance; // Reference to the game manager
    }

    // Cycles through his day: goes to job, exhausts
    public IEnumerator CycleDay()
    {
        // If isn't tired -> goes to school or to job

        if (house == null) // Doesn't job if he doesn't have a house
        {
            isTired = true;
            gm.ressources[3] -= 1;

            // Seek for house
        }
        else StartCoroutine(GoHouse()); //va a sa maison
        yield return new WaitForSeconds(5f); //------- à ajuster : 1/4 du jour défini dans eventmanager
        if (gm.ressources[2] > 0)
            gm.ressources[2] -= foodAmount;
        else
            Die();

        if (!isTired) StartCoroutine(GoWork());

    }

	public void Die() {
        // Character dies
        // TODO: plays animation and despawns
        gm.ressources[3] -= 1;
		Destroy(gameObject); // Self destruction
    }

    public IEnumerator GoWork()
    {
        float t = 0f;
        if(job == null || job == gm.jobs[3])
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, new Vector2(transform.position.x-100, transform.position.y), t / travelDuration); //permet de créer un déplacement fluide
            yield return null;
        }
        else
        {
            while (t < travelDuration)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, job.transform.position, t / travelDuration); //permet de créer un déplacement fluide
                yield return null;
            }
        }
        //transform.Translate(job.transform.position);
        //transform.position = job.transform.position;
        //job.peopleWorking++; // Arrived at work
        // Work for some time
    }
    public IEnumerator GoHouse()
    {
        float t = 0f;
        while (t < travelDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, house.transform.position, t / travelDuration);
            yield return null;
        }
        //transform.Translate(house.transform.position);
        //transform.position = house.transform.position;
        //job.peopleWorking--; // Quit his work
    }
}