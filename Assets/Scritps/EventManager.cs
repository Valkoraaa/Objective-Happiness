using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private bool paused = false;
    public static event Action EndOfDay;
    public Sprite[] dayIcons;
    public Image displayedIcon;
    public int dayLenght;
    
    void Start() 
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

    IEnumerator TriggerEventDayPast()
    {
        float timer = 0f;

        while (true)
        {
            if (!paused)
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
    IEnumerator UpdateDayUi()
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
                    //EndOfDay?.Invoke(); Why??
                    timer = 0f;
                    dayStep ++;
                }
            }

            yield return null;
        }
    }
}