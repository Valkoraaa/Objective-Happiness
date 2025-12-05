using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private bool paused = false;
    public static event Action EndOfDay;
    
    void Start() { StartCoroutine(TriggerEventEvery30Seconds()); }

    public void PauseGame()
    {
        paused = !paused;
        Debug.Log("Pause Game");
    }

    IEnumerator TriggerEventEvery30Seconds()
    {
        float timer = 0f;

        while (true)
        {
            if (!paused)
            {
                timer += Time.deltaTime;

                if (timer >= 5f)
                {
                    EndOfDay?.Invoke();
                    Debug.Log("Event déclenché !");
                    timer = 0f;
                }
            }

            yield return null;
        }
    }
}