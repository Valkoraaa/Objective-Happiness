using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public List<Character> charaAlive;
    public List<House> houses;
    public static GameManager Instance;
    public GameObject characterPrefab;
    public GameObject[] jobs;   // assign�s dans l�inspecteur

    public int food;
    public int wood;
    public int rocks;
    public int prosperity;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(characterPrefab);
            Character c = obj.GetComponent<Character>();

            c.Init(jobs[i], c.house.gameObject);   // Peut etre temporaire
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
        GameObject obj = Instantiate(characterPrefab);
        Character c = obj.GetComponent<Character>();

        c.Init(null, c.house.gameObject);   // Peut etre temporaire
    }
}