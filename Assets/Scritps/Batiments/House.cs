using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool isTaken;
    public GameManager gm;
    void OnEnable()
    {
        StartCoroutine(WaitForGM());
    }

    IEnumerator WaitForGM()
    {
        while(GameManager.Instance == null)
        {
            yield return null;
        }
        gm = GameManager.Instance;
        gm.houses.Add(this);
        if (!isTaken)
        {
            foreach (Character chara in gm.charaAlive)
            {
                if (chara.house == null && !isTaken)
                {
                    chara.house = this.gameObject;
                    isTaken = true;
                    chara.transform.position = transform.position;
                    chara.isTired = false;
                }
            }
        }
    }
}
