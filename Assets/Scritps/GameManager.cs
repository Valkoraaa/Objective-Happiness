using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Character> charaAlive;
    public int food;
    public int wood;
    public int rocks;
    public int prosperity;
    public static GameManager Instance;
    public GameObject characterPrefab;

    private void Awake()
    {
        Instance = this;

        GameObject obj = Instantiate(characterPrefab);
        Character c = obj.GetComponent<Character>();

        // for(int i = 0; i < 5; i++)
        // {
        //     c.Init(c.job, c.home);
        // }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

	void OnEnable() // active consommation quand EnfOfDay est appel�
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
    } //EST MIS EN COMMENTAIRE LE TEMPS QUE CHARACTER SOIT IMPLEMENTER POUR PAS FAIRE PLANTER
}