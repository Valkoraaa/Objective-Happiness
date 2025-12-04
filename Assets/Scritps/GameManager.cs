using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Character> charaAlive;
    public List<House> houses;
    public static GameManager Instance;
    public GameObject characterPrefab;

    public int food;
    public int wood;
    public int rocks;
    public int prosperity;

    private void Awake()
    {
        Instance = this;

        GameObject obj = Instantiate(characterPrefab); // Initialising prefab
        Character c = obj.GetComponent<Character>(); // Getting its script

        for(int i = 0; i < 5; i++)
        {
            c.Init(c.job.gameObject, c.house.gameObject); // Initialises his job and house. NOT SUITABLE FOR NEWBORN CHARACTERS
        }
    }

	void OnEnable() { EventManager.EndOfDay += Evening; }
    void OnDisable() { EventManager.EndOfDay -= Evening; }

    void Evening()
    {
        foreach (Character chara in charaAlive)
        {
            chara.CycleDay();
        }
    } //EST MIS EN COMMENTAIRE LE TEMPS QUE CHARACTER SOIT IMPLEMENTER POUR PAS FAIRE PLANTER
}