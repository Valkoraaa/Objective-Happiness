using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject job;
    public GameObject house;
    private GameManager gm;
    public bool hasSkin = false;
    private float travelDuration = 1;
    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();
    public EventManager em;

    [SerializeField] private int foodAmount = 1; // The needed amount of food to survive

    public bool isTired = false;

    // Initialises game character by passing the game objects and accessing their scripts
    public void Init() // Newborn function which
    {
        job = null;
        house = null;
    }
    public void Init(GameObject actualJob, GameObject actualHouse)
    {
        job = actualJob;
        house = actualHouse;
        gm = GameManager.Instance; // Reference to the game manager
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    private void Awake()
    {
        gm = GameManager.Instance;
        em = EventManager.InstanceEvent;
    }


    // Cycles through his day: goes to job, exhausts
    public IEnumerator CycleDay()
    {
        // If isn't tired -> goes to school or to job

        if (house == null) // Doesn't job if he doesn't have a house
        {
            isTired = true;
            gm.ressources[3] -= 3;
            Renderer[] rends = GetComponentsInChildren<Renderer>(true);

            originalColors.Clear();

            foreach (Renderer r in rends)
            {
                originalColors[r] = r.material.color; // stock original colors
                r.material.color = Color.red;         // become red if tired
            }


            // Seeks for house
        }
        else
        {
            foreach (Renderer r in originalColors.Keys)
            {
                if (r != null)
                {
                    r.material.color = originalColors[r];
                }
            }

            StartCoroutine(GoHouse());
        }
        yield return new WaitForSeconds(4f); //------- to adjust : 1/4 of daylenght
        if (gm.ressources[2] > 0)
            gm.ressources[2] -= foodAmount;
        else
            Die();

        if (!isTired) StartCoroutine(GoWork());

    }

    public void Die()
    {
        // Character dies
        Instantiate(em.dieParticles, transform.position, Quaternion.identity);
        gm.ressources[3] -= 10;
        int i = 0;
        foreach (GameObject jobs in gm.jobs)
        {
            if (job == jobs)
            {
                gm.peopleAtWork[i] -= 1;
                gm.peopleWorkingText[i].text = gm.peopleAtWork[i].ToString();
            }
            i++;
        }
        Destroy(gameObject);
    }

    public IEnumerator GoWork()
    {
        float t = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x - 0.4f, startPos.y, startPos.z);
        if (job == null || job == gm.jobs[3])
        {
            while (t < travelDuration)
            {
                if (!em.paused) //check for pause
                {
                    t += Time.deltaTime;
                    transform.position = Vector3.Lerp(startPos, endPos, t / travelDuration); //fuild movement
                }
                yield return null;
            }
        }
        else
        {
            while (t < travelDuration)
            {
                if (!em.paused)
                {
                    t += Time.deltaTime;
                    transform.position = Vector3.Lerp(startPos, job.transform.position, t / travelDuration);
                }
                yield return null;
            }
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
    }
    public IEnumerator GoHouse()
    {
        gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Vector3 startPos = transform.position;
        float t = 0f;
        while (t < travelDuration)
        {
            if (!em.paused)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, house.transform.position, t / travelDuration);
            }
            yield return null;
        }
    }
}