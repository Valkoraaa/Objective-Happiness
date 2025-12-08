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
    public GameObject[] buildings;
    public int[] ressources; // 0 = wood, 1 = rock, 2 = food, 3 = prosperity

    private int[] peopleAtWork = {0,0,0,0}; // 0 = wood, 1 = rock, 2 = food, 3 - builders
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI[] ressourcesText; // 0 = wood, 1 = rock, 2 = food, 3 = population
    [SerializeField] private TextMeshProUGUI[] peopleWorkingText; // 0 = wood, 1 = rock, 2 = food
    
    public List<Character> masonNumber;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(characterPrefab);
            Character c = obj.GetComponent<Character>();

            //c.Init(jobs[i], c.house.gameObject);   // temporary?
            c.Init(jobs[i], null);
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
            for (int i = 0;i < jobs.Length; i++) //-1 for the mason
            {
                if (chara.job == jobs[i] && chara.job != jobs[jobs.Length -1])
                {
                    ressources[i] += 2; // Adding ressources
                    ressourcesText[i].text = ressources[i].ToString(); // Showing the ressources number
                    ressourcesText[3].text = charaAlive.Count.ToString(); // Show how many workers are for this job

                    peopleAtWork[i] += 1;
                    peopleWorkingText[i].text = peopleAtWork[i].ToString();

                    GameObject newModel = Instantiate(skin[i], chara.transform);

                    ressources[i] += 2;
                    if(!chara.hasSkin)
                    {
                        newModel = Instantiate(skin[i], chara.transform);
                        chara.hasSkin = true;
                    }

                    /*newModel.transform.localPosition = Vector3.zero;
                    newModel.transform.localRotation = Quaternion.identity; ------useless for now, delete it in the end if not used
                    newModel.transform.localScale = Vector3.one;*/

                    peopleAtWork[i] = 0; // Reinitialisation of people working at specific place
                }
                else if (!chara.hasSkin && i == jobs.Length - 1)
                {
                    GameObject newModel = Instantiate(skin[i], chara.transform);
                }
                if(chara.job == jobs[3] && !masonNumber.Contains(chara))
                {
                    masonNumber.Add(chara);
                }
            }
        }
        GameObject obj = Instantiate(characterPrefab);
        Character c = obj.GetComponent<Character>();

        c.Init(null, null);   // temporary?
        foreach(House house in houses)
        {
            foreach(Character chara in charaAlive)
            {
                if (house.isTaken == false && chara.house == null)
                {
                    chara.house = house.gameObject;
                    house.isTaken = true;
                }
            }
        }
    }
}