using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool isTaken;
    private GameManager gm;
    void OnEnable()
    {
        gm = GameManager.Instance;
        foreach (Character chara in gm.charaAlive)
        {
            if (chara.house != null)
            {
                chara.house = this;
            }
        }
    }
}
