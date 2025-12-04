/*using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour{
	[SerializeField] private GameObject prefab;
	[SerializeField] private int population = 100;
	private List<Character> characterList = new List<Character>();

	private void Start() {
		Spawn();
	}
	
	// Spawns the population at the start of the game
	private void Spawn() {
		for(int i = 0; i < population; i++) {
			GameObject character = Instantiate(prefab, Vector3.zero, Quaternion.identity);
			// Setting up the parameters
		}
	}

	private void DayStart() {
		foreach(Character character in characterList) {
			if(character.home != null) // If has a home -> not tired anymore
				character.isTired = false;
		}
	}
	private void DayEnd() {
		foreach(Character character in characterList) {
			if(character.job != null || character.school != null) // If he worked or studied
				character.isTired = true; // Tired at the end of the day
		}
	}
}*/