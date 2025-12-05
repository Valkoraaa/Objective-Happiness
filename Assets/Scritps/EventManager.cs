using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action EndOfDay;
    
    void Start() { StartCoroutine(TriggerEventEvery30Seconds()); }

    private IEnumerator TriggerEventEvery30Seconds()
    {
        while (true) // boucle infinie
        {
            yield return new WaitForSeconds(5f); // attends 30 secondes
            EndOfDay?.Invoke();
            Debug.Log("Event d�clench� !");
        }
    }
}