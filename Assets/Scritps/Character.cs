using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Character : MonoBehaviour {
	public GameObject job;
	//public GameObject school; pas sur de l utilit�
	public GameObject home;
	private GameManager gm;

	[SerializeField] private int foodAmount = 1; // The needed amount of food to survive

	public bool isTired = false;

	public Character(GameObject actualJob, GameObject actualHome)
	{
		job = actualJob;
		home = actualHome;
	}

	[SerializeField] private GameManager gameManager; // For optimization purposes: !!! DO NOT USE GETCOMPONENT<>() FUNCTION !!! CHOOSE THE GAME OBJECT IN THE EDITOR

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public void CycleDay() {
        // If isn't tired -> goes to school or to job

        if (home == null)
        {
            isTired = true;
            gameManager.prosperity -= 1;
        }
        else { GoHome(); } //va a sa maison
        if (gameManager.food > 0)
        {
            gameManager.food -= foodAmount;
        }
        else { Die(); }
        if(!isTired)
        {
            //aller au travail
        }
	}

	public void Die() {
		// Character dies
		// TODO: plays animation and despawns
		Destroy(gameObject); // Self destruction
        gm.prosperity -= 1;
    }

	public void GoHome() {

	}
}