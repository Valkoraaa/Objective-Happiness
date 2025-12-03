using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTerrain : MonoBehaviour
{
    public GameObject school;
    public GameObject farm;
    public GameObject museum;
    public GameObject library;
    public GameObject house;
    private GameObject choosenBuilding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Vérifie le tag de l’objet touché
                if (hit.collider.CompareTag("constructible"))
                {
                    Debug.Log("Objet constructible cliqué : " + hit.collider.name);
                    Instantiate(school, hit.collider.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
