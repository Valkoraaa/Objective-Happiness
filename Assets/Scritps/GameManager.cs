using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public List<Character> charaAlive;
    public int food;
    public int prosperity;
    public static GameManager Instance;
    public GameObject characterPrefab;
    public GameObject[] jobs;   // assignés dans l’inspecteur

    private void Awake()
    {
        Instance = this;

        
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(characterPrefab);
            Character c = obj.GetComponent<Character>();

            c.Init(jobs[i], c.home);   // Peut etre temporaire
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

	void OnEnable() // active consommation quand EnfOfDay est appelé
    {
        EventManager.EndOfDay += Evening;
    }

    void OnDisable()
    {
        EventManager.EndOfDay -= Evening;
    }

    void Evening()
    {
        foreach (Character chara in charaAlive)
        {
            chara.CycleDay();
            
        }
        GameObject obj = Instantiate(characterPrefab);
        Character c = obj.GetComponent<Character>();

        c.Init(null, c.home);   // Peut etre temporaire
    }
}
