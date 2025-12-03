using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	private GameObject job;
	private GameObject school;
	private GameObject home;

	private bool isTired = false;
	private bool isHungry = false;

	public void Work() {
		// If isn't tired -> goes to school or to job
		if(!isTired) {
			if(job != null) {
				// Goes to job
				isTired = true;
			} else {
				// Goes to school
				isTired = true;
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
			// TODO: check for available food
		} else {
			// Reduces hapiness bar
			Kill();
		}
	}

	public void Kill() {
		// Character dies
		// TODO: plays animation and despawns
		Destroy(gameObject); // Self destruction
	}
}