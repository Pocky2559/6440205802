using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildCannonOnWall : MonoBehaviour
{
    public GameObject kartaune;
    public GameObject positionToPlace;
    public ResourcesStatus resourcesStatus;
    public UnitDatabaseSO unitDatabase;
    public HouseList population;
    public GameObject icon;
    public KartauneStateController kartauneStateController;
    public bool IsCannonDestroy;
    [SerializeField] private GameObject kartauneBuildPanel;
    [SerializeField] private SoundEffectController soundEffectController;

    public void BuildCannon()
    {
        if(resourcesStatus.food_Amount >= unitDatabase.unitDetails[4].foodCost
           && resourcesStatus.gold_Amount >= unitDatabase.unitDetails[4].goldCost
           && population.currentPopulation + unitDatabase.unitDetails[4].population <= population.currentPopulationCapacity
           && GameObject.FindGameObjectWithTag("Artillary") == true)
        {
            resourcesStatus.ResourcesChange("Food", -(unitDatabase.unitDetails[4].foodCost));
            resourcesStatus.ResourcesChange("Gold", -(unitDatabase.unitDetails[4].goldCost));
            population.PopulationChanges(unitDatabase.unitDetails[4].population);
            GameObject Kartaune = Instantiate(kartaune, positionToPlace.transform.position, Quaternion.Euler(0,-180,0));
            kartauneStateController = Kartaune.GetComponentInChildren<KartauneStateController>();
            IsCannonDestroy = false;
            kartauneStateController.CannonOnWall(this);
            positionToPlace.SetActive(false);
            icon.SetActive(false);
            kartauneBuildPanel.SetActive(false);
            soundEffectController.PlayPlaceBuildingSound();//Play Place Building Sound
        }
    }
}
