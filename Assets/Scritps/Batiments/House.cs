using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool isTaken;
    public GameManager gm;
    /*void OnEnable()
    {
        gm = GameManager.Instance;
        foreach (Character chara in gm.charaAlive)
        {
            if (chara.house != null)
            {
                chara.house = this.gameObject;
            }
        }
    }*/
    void Start()
    {
        gm = GameManager.Instance;
        foreach (Character chara in gm.charaAlive)
        {
            if (chara.house != null)
            {
                chara.house = this.gameObject;
            }
        }
    }
}
