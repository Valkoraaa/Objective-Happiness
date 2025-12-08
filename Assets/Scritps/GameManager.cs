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
    public GameObject[] jobs;   
    public GameObject[] skin; // a assigner de la meme facon que l augmentation des ressources
    public GameObject[] buildings;
    public int[] ressources; // 0 = wood, 1 = rock, 2 = food, 3 = prosperity
    private List<Character> masons = new List<Character>();
    public int masonsNumber;
    private bool charaSelected = false;
    public SlidePannel sp;

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
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //vérifie si le joueur clique sur un objet ayant le tag constructible
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Vérifie le tag de l’objet touché
                if (hit.collider.CompareTag("character"))
                {
                    charaSelected = true;
                    Debug.Log("charaCliqué");
                }
                else if (hit.collider.CompareTag("school") && charaSelected)
                {
                    charaSelected = false;
                    sp.Move(true);
                    Debug.Log("SchoolCliqué");
                }
            }
        }
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
                    ressources[i] += 2;
                    if(!chara.hasSkin)
                    {
                        GameObject newModel = Instantiate(skin[i], chara.transform);
                        chara.hasSkin = true;
                    }

                    /*newModel.transform.localPosition = Vector3.zero;
                    newModel.transform.localRotation = Quaternion.identity; ------useless for now, delete it in the end if not used
                    newModel.transform.localScale = Vector3.one;*/
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
        GameObject obj = Instantiate(characterPrefab);
        Character c = obj.GetComponent<Character>();
        masonsNumber = masons.Count;

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