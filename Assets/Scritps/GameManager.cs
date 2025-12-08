using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public List<Character> charaAlive;
    public List<House> houses;
    public GameObject[] jobs;   
    public GameObject[] skin; // a assigner de la meme facon que l augmentation des ressources
    public int[] ressources; // 0 = wood, 1 = rock, 2 = food, 3 = prosperity
    private int[] peopleAtWork = {0,0,0,0}; // 0 = wood, 1 = rock, 2 = food, 3 - builders

    public static GameManager Instance;
    private TextMeshProUGUI[] ressourcesText; // 0 = wood, 1 = rock, 2 = food, 3 = population

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(characterPrefab);
            Character c = obj.GetComponent<Character>();

            //c.Init(jobs[i], c.house.gameObject);   // temporary?
            c.Init(jobs[i], houses[i].gameObject);
            charaAlive.Add(c);
        }
        ;
    }


    void OnEnable() { EventManager.EndOfDay += Evening; }
    void OnDisable() { EventManager.EndOfDay -= Evening; }

    void Evening() //called every evenings
    {
        foreach (Character chara in charaAlive)
        {
            StartCoroutine(chara.CycleDay()); //enable end of day script for each characters
            for (int i = 0;i < jobs.Length - 1; i++) //-1 for the mason
            {
                if (chara.job == jobs[i])
                {
                    ressources[i] += 2; // Adding ressources
                    ressourcesText[i].text = ressources[i].ToString(); // Showing the ressources number
                    ressourcesText[3].text = charaAlive.Count.ToString(); // Show how many workers are for this job
                    
                    GameObject newModel = Instantiate(skin[i], chara.transform);

                    /*newModel.transform.localPosition = Vector3.zero;
                    newModel.transform.localRotation = Quaternion.identity; ------useless for now, delete it in the end if not used
                    newModel.transform.localScale = Vector3.one;*/
                }
            }
        }
        GameObject obj = Instantiate(characterPrefab);
        Character c = obj.GetComponent<Character>();

        c.Init(null, c.house.gameObject);   // temporary?
        Debug.Log("Evening GM");
    }
}