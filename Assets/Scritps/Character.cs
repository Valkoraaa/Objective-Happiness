using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public GameObject job;
	public GameObject school;
	public GameObject home;

	[SerializeField] private int foodAmount = 1; // The needed amount of food to survive

	public bool isTired = false;
	public bool isHungry = false;

	[SerializeField] private Game globalGame; // For optimization purposes: !!! DO NOT USE GETCOMPONENT<>() FUNCTION !!! CHOOSE THE GAME OBJECT IN THE EDITOR

	public void Work() {
		// If isn't tired -> goes to school or to job
		if(!isTired) {
			if(job != null) {
				// Goes to job
			} else {
				// Goes to school
			}
		}

		// If tired -> seeks for shelter
		if(home != null) {
			// Goes home
		} else {
			// Reduces hapiness bar
		}
		
		// If is hungry -> tries to eat, otherwise dies
		if(isHungry) {
			// Eats
			// globalGame.ConsumeFood();
			// TODO: check for available food
			if(globalGame.TotalFood <= foodAmount) { // No food at all -> dies
				// Reduces hapiness bar
				// globalGame.Hapiness -= 10;
				Die();
			}	
		}
	}

	public void Die() {
		// Character dies
		// TODO: plays animation and despawns
		Destroy(gameObject); // Self destruction
	}
}