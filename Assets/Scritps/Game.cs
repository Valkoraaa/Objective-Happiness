// Game.cs is a global game script which manages the hapiness, the food, etc...
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour{
	[SerializeField] private int totalFood = 0;
	public int TotalFood {
		get { return totalFood; }
		set { totalFood = value; }
	}

	[SerializeField] private int hapiness = 0;
	public int Hapiness {
		get { return hapiness; }
		set { hapiness = value; }
	}

	public void ProduceFood(int value) { TotalFood += value; }
	public void ConsumeFood(int value) { TotalFood -= value; }
}