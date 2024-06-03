using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HouseList : MonoBehaviour // X/number
{
    public List<GameObject> houseList;
    public TMP_Text currentPopulationText;
    public int houseCapacityAssign;
    public int currentPopulationCapacity;
    public int currentPopulation;
    public UnitSelection listOfUnits;
    private bool IsCalculatePopAtBeginning;
    [SerializeField] private bool isTutorial;

    private void Start()
    {
        CalculatePopulationAtBeginning();
        if(isTutorial == true)
        {
            currentPopulationCapacity = 5;
        }
    }
    public void AddHouseToList(GameObject house)
    { 
        houseList.Add(house);
        currentPopulationCapacity = houseCapacityAssign * houseList.Count;
    }

    public void DeleteHouseFromList(GameObject house)
    {
        houseList.Remove(house); 
    }

    public void PopulationChanges(int populationChanges)
    {
        currentPopulation = currentPopulation + populationChanges;
    }

    private void Update()
    {
        currentPopulation = listOfUnits.unitList.Count;
        currentPopulationText.text = string.Format("{0}/{1}", currentPopulation, currentPopulationCapacity);
    }

    private void CalculatePopulationAtBeginning()
    {
        currentPopulationCapacity = houseCapacityAssign * houseList.Count;
    }
}
