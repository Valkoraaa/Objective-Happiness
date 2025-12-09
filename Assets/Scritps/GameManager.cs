using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public List<Character> charaAlive;
    public List<House> houses;
    public GameObject[] jobs;   
    public GameObject[] skin; // a assigner de la meme facon que l augmentation des ressources
    public GameObject[] buildings;
    public int[] ressources; // 0 = wood, 1 = rock, 2 = food, 3 = prosperity
    private List<Character> masons = new List<Character>();
    public int masonsNumber;
    private bool charaSelected = false;
    public SlidePannel sp;
    private int[] peopleAtWork = {0,0,0,0}; // 0 = wood, 1 = rock, 2 = food, 3 - builders
    public static GameManager Instance;
    public Character changingChara;
    private int tilesFree = 69;
    public EventManager em;
    public Canvas winCanvas;
    public Canvas loseCanvas;
    public UnityEngine.UI.Slider prospertySlider;
    private int nightCount = 0;

    [SerializeField] private TextMeshProUGUI[] ressourcesText; // 0 = wood, 1 = rock, 2 = food, 3 = population
    [SerializeField] private TextMeshProUGUI[] peopleWorkingText; // 0 = wood, 1 = rock, 2 = food
    [SerializeField] private TextMeshProUGUI masonsText;
    
    public List<Character> masonNumber;


    private void Awake()
    {
        Instance = this;
        em = EventManager.InstanceEvent;
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(characterPrefab);
            Character c = obj.GetComponent<Character>();

            //c.Init(jobs[i], c.house.gameObject);   // temporary?
            c.Init(jobs[i], null);
            charaAlive.Add(c);
        }
    }
    void Update()
    {
        prospertySlider.value = ressources[3];
        if (Input.GetMouseButtonDown(0)) //v�rifie si le joueur clique sur un objet ayant le tag constructible
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // V�rifie le tag de l�objet touch�
                if (hit.collider.CompareTag("character"))
                {
                    charaSelected = true;
                    Debug.Log("charaCliqu�");
                    changingChara = hit.collider.GetComponent<Character>();
                }
                else if (hit.collider.CompareTag("school") && charaSelected)
                {
                    charaSelected = false;
                    sp.Move(true);
                    Debug.Log("SchoolCliqu�");
                }
            }
            if (ressources[3] >= 250) //win
            {
                em.paused = true;
                ressources[3] = 0;
                winCanvas.gameObject.SetActive(true);
            }
            else if (charaAlive.Count <= 1 || tilesFree<=0) //lose
            {
                em.paused = true;
                ressources[3] = 0;
                loseCanvas.gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            // Ressources labels update
            ressourcesText[i].text = ressources[i].ToString(); // Showing the ressources number
            ressourcesText[3].text = charaAlive.Count.ToString(); // Show how many workers are for this job
        }
        
        // Counting builders number
        masonsNumber = masons.Count;
        masonsText.text = masonsNumber.ToString();
    }

    public int MapFull()
    {
        tilesFree -= 1;
        return tilesFree;
    }

    public void ChangeJob(GameObject changeJob)
    {
        changingChara.job = changeJob;
        changingChara.hasSkin = false;
        foreach (Transform child in changingChara.transform)
        {
            Destroy(child.gameObject);
        }
        int i = 0;
        foreach (GameObject build in jobs)
        {
            if (build == changeJob)
            {
                break;
            }
            i++;
        }
        GameObject newModel = Instantiate(skin[i], changingChara.transform);
        changingChara.hasSkin = true;
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

                    // Ressources labels update
                    ressourcesText[i].text = ressources[i].ToString(); // Showing the ressources number
                    ressourcesText[3].text = charaAlive.Count.ToString(); // Show how many workers are for this job

                    peopleAtWork[i] += 1;
                    peopleWorkingText[i].text = peopleAtWork[i].ToString();

                    //GameObject newModel = Instantiate(skin[i], chara.transform);

                    ressources[i] += 2;
                    if(!chara.hasSkin)
                    {
                        foreach (Transform child in chara.transform)
                        {
                            Destroy(child.gameObject);
                        }
                        GameObject newModel = Instantiate(skin[i], chara.transform);
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
                if(chara.job == jobs[3] && !masons.Contains(chara))
                {
                    masons.Add(chara);
                }
            }
        }
        if (charaAlive.Count >= 2 && nightCount%3==0)
        {
            GameObject obj = Instantiate(characterPrefab);
            Character c = obj.GetComponent<Character>();

            c.Init(null, null);   // temporary?
            GameObject homeLess = Instantiate(skin[4], c.transform);
            c.hasSkin = true;
            c.transform.position = new Vector3(UnityEngine.Random.Range(-3f, 3f), 0f, UnityEngine.Random.Range(-0.4f, 6.5f));

            charaAlive.Add(c);
        }
        nightCount++;
        
        foreach (House house in houses)
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