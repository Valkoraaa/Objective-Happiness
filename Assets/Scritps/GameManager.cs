// GameManager.cs manages the life of characters, creating new characters and ressources
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game world parameters")]
    public List<Character> charaAlive;
    public List<GameObject> freeHouses; // Houses that are free, we will delete them from the list if the character occupies it
    public GameObject characterPrefab;
    public GameObject[] jobs; // Initial available jobs and the beggining of the game

    [Space]

    [Header("Game system parameters")]
    public int food;
    public int wood;
    public int rocks;
    public int prosperity;

    [HideInInspector] public static GameManager Instance;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < 5; i++) // Firstly, we add 5 characters by the start of the game
        {
            Character c = Instantiate(characterPrefab).GetComponent<Character>();
            c.Init(jobs[i], freeHouses[i]); // Applying default jobs and houses
        }
    }

	void OnEnable() { EventManager.EndOfDay += Evening; }
    void OnDisable() { EventManager.EndOfDay -= Evening; }

    void Evening() //called every evenings
    {
        foreach (Character chara in charaAlive)
            chara.CycleDay(); // Works, getting tired, returns home

        Character c = Instantiate(characterPrefab).GetComponent<Character>();
        c.Init(); // Each evening, new character borns, without anything

        Debug.Log("Evening GM");
    }
}