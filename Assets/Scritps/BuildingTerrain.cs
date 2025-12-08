using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingTerrain : MonoBehaviour
{
    /*public GameObject school;
    public GameObject farm;
    public GameObject museum;
    public GameObject library;
    public GameObject house;*/
    private GameObject choosenBuilding;
    private bool wantsToBuild = false;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            wantsToBuild = false;
        }
        if (Input.GetMouseButtonDown(0)) //vérifie si le joueur clique sur un objet ayant le tag constructible
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Vérifie le tag de l’objet touché
                if (hit.collider.CompareTag("constructible") && wantsToBuild)
                {
                    for (int i = 0; i < gm.buildings.Length; i++)
                    {
                        Debug.Log("check1");
                        if (choosenBuilding == gm.buildings[i])
                        {
                            Debug.Log("check2");
                            bool canBuild = false;
                            if (i == 0 && gm.ressources[0]>=30 && gm.ressources[1]>=20 && gm.masonNumber.Count>=3)
                            {
                                gm.ressources[0]-=30;
                                gm.ressources[1]-=20;
                                canBuild = true;
                            }
                            else if (i == 1 && gm.ressources[0]>=10 && gm.ressources[1]>= 10 && gm.masonNumber.Count >= 1)
                            {
                                gm.ressources[0]-=10;
                                gm.ressources[1]-=10;
                                canBuild = true;
                            }
                            else if (i == 2 && gm.ressources[0] >= 30 && gm.ressources[1] >= 30 && gm.masonNumber.Count >= 2)
                            {
                                gm.ressources[0] -= 30;
                                gm.ressources[1] -= 30;
                                canBuild = true;
                            }
                            else if (i == 3 && gm.ressources[0] >= 50 && gm.ressources[1] >= 50 && gm.masonNumber.Count >= 3)
                            {
                                gm.ressources[0] -= 50;
                                gm.ressources[1] -= 50;
                                canBuild = true;
                            }
                            else if (i == 4 && gm.ressources[0] >= 30 && gm.ressources[1] >= 30 && gm.masonNumber.Count >= 1)
                            {
                                gm.ressources[0] -= 30;
                                gm.ressources[1] -= 30;
                                canBuild = true;
                            }
                            if(canBuild)
                            {
                                Debug.Log("Objet constructible cliqué : " + hit.collider.name);
                                Instantiate(choosenBuilding, hit.collider.transform.position, Quaternion.identity);
                                hit.collider.tag = "nonConstructible";
                            }
                            else { Debug.Log("erreur"); }
                        }
                    }
                }
            }
        }
    }

    public void ChooseBuilding(GameObject building)
    {
        choosenBuilding = building;
        wantsToBuild = true;
    }
}
