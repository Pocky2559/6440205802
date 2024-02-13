using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetailsUI : MonoBehaviour
{
    public UnitSelection unitSelection;
    public GameObject unitDetailsUI;
    public GameObject thisGameObject;
    public UnitStat unitStat;
    public HouseList population;
    public UnitDatabaseSO unitDatabase;

    public TMP_Text unitName;
    public TMP_Text unitAttackDetail;
    public TMP_Text unitHPDetail;
    public TMP_Text unitMeleeArmorDetail;
    public TMP_Text unitRangedArmorDetail;


    private void Awake()
    {
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();    
    }
    private void Update()
    {
       CheckSelection();
    }

    private void CheckSelection()
    {
        if(unitSelection.unitSelected.Count == 1)
        {
                unitDetailsUI.SetActive(true); // show detail UI
            try
            {
                unitStat = unitSelection.unitSelected[0].GetComponent<UnitStat>();
                thisGameObject = unitSelection.unitSelected[0].gameObject;
                unitName.text = unitStat.unitName;
                unitAttackDetail.text = "Attack : " + unitStat.unitDamage.ToString();
                unitHPDetail.text = "HP : " + unitStat.unitHP.ToString();
                unitMeleeArmorDetail.text = "Melee Armor : " + unitStat.unitMeleeArmor.ToString();
                unitRangedArmorDetail.text = "Ranged Armor : " + unitStat.unitRangedArmor.ToString();
            }
            catch
            {
                Debug.Log("Unit has been destroy");
                unitDetailsUI.SetActive(false);
            }
               
        }
        else
        {
            unitDetailsUI.SetActive(false); // hide detail UI
        }
    }

    public void DeleteUnit()
    {
        unitSelection.DeselectAll();

        #region Remove Unit
        if (thisGameObject.CompareTag("Villager"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[0].population);
            Destroy(unitStat.gameObject);
        }

        if (thisGameObject.CompareTag("Gunner"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[1].population);
            Destroy(unitStat.gameObject);
        }

        if (thisGameObject.CompareTag("Landsknecht"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[2].population);
            Destroy(unitStat.gameObject);
        }

        if (thisGameObject.CompareTag("Captain"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[3].population);
            Destroy(unitStat.gameObject);
        }

        if (thisGameObject.CompareTag("Kartouwe"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[4].population);
            Destroy(unitStat.gameObject);
        }
        #endregion
    }
}
