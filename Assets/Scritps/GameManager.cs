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
    [Header("Character related")]
    public GameObject characterPrefab;
    public List<Character> charaAlive;
    public List<House> houses;
    public GameObject[] jobs;   
    public GameObject[] skin;

    [Header("Game related")]
    public GameObject[] buildings;
    public int[] ressources; // 0 = wood, 1 = rock, 2 = food, 3 = prosperity
    private List<Character> masons = new List<Character>();
    public int masonsNumber;
    private bool charaSelected = false;
    public SlidePannel sp;
    public int[] peopleAtWork = {1,1,1,1}; // 0 = wood, 1 = rock, 2 = food, 3 - builders
    public static GameManager Instance;
    public Character changingChara;
    private int tilesFree = 69;
    public EventManager em;
    private int nightCount = 0;

    [Header("UI")]
    public Canvas winCanvas;
    public Canvas loseCanvas;
    public UnityEngine.UI.Slider prospertySlider;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip buildSound;
    public AudioClip failSound;

    [SerializeField] private TextMeshProUGUI[] ressourcesText; // 0 = wood, 1 = rock, 2 = food, 3 = population
    [SerializeField] public TextMeshProUGUI[] peopleWorkingText; // 0 = wood, 1 = rock, 2 = food
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
            c.Init(jobs[i], null);
            charaAlive.Add(c);
        }
    }
    void Update()
    {
        prospertySlider.value = ressources[3];
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //check where the player clicks
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Verify tag of hitten object
                if (hit.collider.CompareTag("character"))
                {
                    charaSelected = true;
                    Debug.Log("charaCliqu�");
                    changingChara = hit.collider.GetComponent<Character>();
                    audioSource.PlayOneShot(clickSound);
                }
                else if (hit.collider.CompareTag("school") && charaSelected)
                {
                    charaSelected = false;
                    sp.Move(true);
                    Debug.Log("SchoolCliqu�");
                    audioSource.PlayOneShot(clickSound);
                }
            }
            if (ressources[3] >= 250) //win
            {
                em.paused = true;
                ressources[3] = 0;
                winCanvas.gameObject.SetActive(true);
                audioSource.PlayOneShot(winSound);
            }
            else if (charaAlive.Count <= 1 || tilesFree<=0) //lose
            {
                em.paused = true;
                ressources[3] = 0;
                loseCanvas.gameObject.SetActive(true);
                audioSource.PlayOneShot(loseSound);
            }
        }
        
        peopleWorkingText[3].text = masonsNumber.ToString();

        for (int i = 0; i < 3; i++)
        {
            // Ressources labels update
            ressourcesText[i].text = ressources[i].ToString(); // Showing the ressources number
            ressourcesText[3].text = charaAlive.Count.ToString(); // Show how many workers are for this job
        }
        peopleWorkingText[3].text = masonsNumber.ToString();
    }

    public int MapFull()
    {
        tilesFree -= 1;
        return tilesFree;
    }

    public void ChangeJob(GameObject changeJob)
    {
        int i = 0;
        foreach (GameObject build in jobs)
        {
            if (build == changeJob)
            {
                peopleAtWork[i] += 1;
                peopleWorkingText[i].text = peopleAtWork[i].ToString();
            }
            if (build == changingChara.job)
            {
                peopleAtWork[i] -= 1;
                peopleWorkingText[i].text = peopleAtWork[i].ToString();
            }
            i++;
        }
        changingChara.job = changeJob;
        changingChara.hasSkin = false;
        foreach (Transform child in changingChara.transform)
        {
            Destroy(child.gameObject);
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
            for (int i = 0;i < jobs.Length; i++)
            {
                if (chara.job == jobs[i] && chara.job != jobs[jobs.Length -1])
                {
                    ressources[i] += 2; // Adding ressources

                    // Ressources labels update
                    ressourcesText[i].text = ressources[i].ToString(); // Showing the ressources number
                    ressourcesText[3].text = charaAlive.Count.ToString(); // Show how many workers are for this job

                    ressources[i] += 2;
                    if(!chara.hasSkin) //enable skin changes for new work or if someone is no longer tired
                    {
                        foreach (Transform child in chara.transform)
                        {
                            Destroy(child.gameObject);
                        }
                        GameObject newModel = Instantiate(skin[i], chara.transform);
                        chara.hasSkin = true;
                    }
                    peopleWorkingText[i].text = peopleAtWork[i].ToString();
                }
                else if (!chara.hasSkin && i == jobs.Length - 1)
                {
                    GameObject newModel = Instantiate(skin[i], chara.transform);
                }
                if(chara.job == jobs[3] && !masons.Contains(chara))
                {
                    masons.Add(chara);
                    
                }
                masonsNumber = masons.Count; //reset available masons number
            }
        }
        if (charaAlive.Count >= 2 && nightCount%3==0) //spawn a new character every 3 nights
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
        
        foreach (House house in houses) //give houses to characters that don t have one
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