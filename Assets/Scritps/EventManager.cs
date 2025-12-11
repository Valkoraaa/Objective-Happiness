using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public bool paused = false;
    public static event Action EndOfDay;
    public Sprite[] dayIcons;
    public Image displayedIcon;
    public int dayLenght;
    public static EventManager InstanceEvent;
    public GameObject dieParticles;

    private void Awake()
    {
        InstanceEvent = this;
    }
    void Start() //start all coroutines and the first day
    {
        StartCoroutine(TriggerEventDayPast());
        StartCoroutine(UpdateDayUi());
        EndOfDay?.Invoke();
    }

    public void PauseGame()
    {
        paused = !paused;
        Debug.Log("Pause Game");
    }



    IEnumerator TriggerEventDayPast() //check for end of day
    {
        float timer = 0f;

        while (true)
        {
            if (!paused) //check for pause
            {
                timer += Time.deltaTime;

                if (timer >= dayLenght)
                {
                    EndOfDay?.Invoke();
                    timer = 0f;
                }
            }

            yield return null;
        }
    }
    IEnumerator UpdateDayUi() //changes ui each 1/4 of the day
    {
        float timer = 0f;
        int dayStep = 0;

        while (true)
        {
            if (!paused)
            {
                if(dayStep == 4)
                {
                    dayStep = 0;
                }
                timer += Time.deltaTime;
                displayedIcon.sprite = dayIcons[dayStep];
                if (timer >= (dayLenght/4))
                {
                    timer = 0f;
                    dayStep ++;
                }
            }

            yield return null;
        }
    }
}