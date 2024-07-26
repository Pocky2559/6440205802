using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExceedPopulation : MonoBehaviour
{
    [SerializeField] private HouseList population;
    [SerializeField] private GameObject notification;

    private void Awake()
    {
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
    }

    void Update()
    {
        if(population.currentPopulation > population.currentPopulationCapacity)
        {
            notification.SetActive(true);
        }

        else
        {
            notification.SetActive(false);
        }
    }
}
