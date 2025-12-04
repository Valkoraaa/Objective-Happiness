using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action EndOfDay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TriggerEventEvery30Seconds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TriggerEventEvery30Seconds()
    {
        while (true) // boucle infinie
        {
            yield return new WaitForSeconds(30f); // attends 30 secondes
            EndOfDay.Invoke();
            Debug.Log("Event déclenché !");
        }
    }
}
