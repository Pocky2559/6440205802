using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HouseList : MonoBehaviour // X/number
{
    public List<GameObject> houseList;
    public TMP_Text currentPopulationText;
    public int houseCapacityAssign;
    public int currentHouseCapacity;
    public int currentPopulation;
    public UnitSelection listOfUnits;
    private bool IsCalculatePopAtBeginning;

    public void AddHouseToList(GameObject house)
    { 
        houseList.Add(house);
        currentHouseCapacity = houseCapacityAssign * houseList.Count;
    }

    public void DeleteHouseFromList(GameObject house)
    {
        houseList.Remove(house); 
    }

    public void PopulationChanges(int populationChanges)
    {
        currentPopulation = currentPopulation + populationChanges;
        Debug.Log("Current Pop : " + currentPopulation);
    }

    private void Update()
    {
        if(IsCalculatePopAtBeginning == false)
        {
            CalculatePopulationAtBeginning();
            IsCalculatePopAtBeginning = true;
        }

        
        currentPopulationText.text = string.Format("{0}/{1}", currentPopulation, currentHouseCapacity);
    }

    private void CalculatePopulationAtBeginning()
    {
        currentPopulation = listOfUnits.unitList.Count;
        Debug.Log("Population : " + currentPopulation);
    }

}
