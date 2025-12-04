using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Character> charaAlive;
    public int food;
    public int prosperity;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
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
    } //EST MIS EN COMMENTAIRE LE TEMPS QUE CHARACTER SOIT IMPLEMENTER POUR PAS FAIRE PLANTER
}
