using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Character> charaAlive;
    public int food;
    public int prosperity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnEnable() // active consommation quand EnfOfDay est appelé
    {
        EventManager.EndOfDay += Consommation;
    }

    void OnDisable()
    {
        EventManager.EndOfDay -= Consommation;
    }

    void Consommation()
    {
        foreach (Character chara in charaAlive)
        {
            if(chara.home == null)
            {
                chara.job = null; //vagabon
                prosperity -= 1;
            }
            else { chara.GoHome(); } //va a sa maison
            if (food > 0)
            {
                food -= 1;
            }
            else { chara.Die(); }

            
        }
    } //EST MIS EN COMMENTAIRE LE TEMPS QUE CHARACTER SOIT IMPLEMENTER POUR PAS FAIRE PLANTER
}
