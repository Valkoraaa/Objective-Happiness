using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public GameObject[] selectedInfo;
    public GameObject escapeInfo;
    public GameObject failParticle;
    public GameObject succedParticle;
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
            Debug.Log("falseBuild");
            foreach (GameObject select in selectedInfo)
            {
                select.SetActive(false);
            }
            escapeInfo.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0)) //v�rifie si le joueur clique sur un objet ayant le tag constructible
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // V�rifie le tag de l�objet touch�
                if (hit.collider.CompareTag("constructible") && wantsToBuild)
                {
                    Debug.Log("check");
                    for (int i = 0; i < gm.buildings.Length; i++)
                    {
                        Debug.Log("check1");
                        if (choosenBuilding == gm.buildings[i])
                        {
                            Debug.Log("check2");
                            bool canBuild = false;
                            if (i == 0 && gm.ressources[0]>=30 && gm.ressources[1]>=20 && gm.masonsNumber>=3) // Farm
                            {
                                gm.ressources[0]-=30;
                                gm.ressources[1]-=20;
                                gm.ressources[2] += 100;
                                canBuild = true;
                                gm.masonsNumber -= 3;
                                gm.MapFull();
                            }
                            else if (i == 1 && gm.ressources[0]>=10 && gm.ressources[1]>= 10 && gm.masonsNumber >= 1) // House
                            {
                                gm.ressources[0]-=10;
                                gm.ressources[1]-=10;
                                canBuild = true;
                                gm.masonsNumber -= 1;
                                gm.MapFull();
                            }
                            else if (i == 2 && gm.ressources[0] >= 30 && gm.ressources[1] >= 30 && gm.masonsNumber >= 2) // Library
                            {
                                gm.ressources[0] -= 30;
                                gm.ressources[1] -= 30;
                                canBuild = true;
                                gm.masonsNumber -= 2;
                                gm.ressources[3] += 40;
                                gm.MapFull();
                            }
                            else if (i == 3 && gm.ressources[0] >= 50 && gm.ressources[1] >= 50 && gm.masonsNumber >= 3) // Museum
                            {
                                gm.ressources[0] -= 50;
                                gm.ressources[1] -= 50;
                                gm.ressources[3] += 75;
                                canBuild = true;
                                gm.masonsNumber -= 3;
                                gm.MapFull();
                            }
                            else if (i == 4 && gm.ressources[0] >= 30 && gm.ressources[1] >= 30 && gm.masonsNumber >= 1) // School
                            {
                                gm.ressources[0] -= 30;
                                gm.ressources[1] -= 30;
                                canBuild = true;
                                gm.masonsNumber -= 1;
                                gm.MapFull();
                            }
                            if(canBuild)
                            {
                                StartCoroutine(Build(hit));
                            }
                            else { gm.audioSource.PlayOneShot(gm.failSound); Instantiate(failParticle, hit.collider.transform.position, Quaternion.identity); }
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
        escapeInfo.SetActive(true);
        gm.audioSource.PlayOneShot(gm.clickSound);
    }

    private IEnumerator Build(RaycastHit hit)
    {
        gm.audioSource.PlayOneShot(gm.buildSound);
        Instantiate(succedParticle, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 1, hit.collider.transform.position.z - 1f), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Objet constructible cliqu� : " + hit.collider.name);
        Instantiate(choosenBuilding, hit.collider.transform.position, Quaternion.identity);
        hit.collider.tag = "nonConstructible";
        
    }
}
