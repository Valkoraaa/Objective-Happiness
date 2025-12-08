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
    // Start is called before the first frame update
    void Start()
    {
        
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
                    Debug.Log("Objet constructible cliqué : " + hit.collider.name);
                    Instantiate(choosenBuilding, hit.collider.transform.position, Quaternion.identity);
                    hit.collider.tag = "nonConstructible";
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
