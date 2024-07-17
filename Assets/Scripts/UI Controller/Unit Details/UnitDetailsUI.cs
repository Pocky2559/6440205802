using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetailsUI : MonoBehaviour
{
    public UnitSelection unitSelection;
    public GameObject unitDetailsUI;
    public GameObject thisGameObject;
    public GameObject enemySelectionIndicator;
    public GameObject removeButton;
    public UnitStat unitStat;
    public HouseList population;
    public UnitDatabaseSO unitDatabase;
    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;
    public LayerMask groundLayerMask;
    private bool isSelectEnemy;
    private bool isSelectUnitSoundPlay;
    private RaycastHit hit;
    public SoundEffectController soundEffectController;

    public TMP_Text unitName;
    public TMP_Text unitAttackDetail;
    public TMP_Text unitHPDetail;
    public TMP_Text unitMeleeArmorDetail;
    public TMP_Text unitRangedArmorDetail;
    


    private void Awake()
    {
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
        isSelectEnemy = false;
    }
    private void Update()
    {
       if(unitSelection.unitSelected.Count >= 1) //if select player unit
       {
          CheckSelection();
          if (isSelectUnitSoundPlay == false)
          {
              soundEffectController = unitSelection.unitSelected[0].GetComponentInChildren<SoundEffectController>();
              soundEffectController.PlayBuildingOrUnitSelectionSound();
              isSelectUnitSoundPlay = true;
          }
       }

       else if (isSelectEnemy == true)
       {
          SelectEnemy();
       }

       else
       {
          unitDetailsUI.SetActive(false);
          isSelectUnitSoundPlay = false;
       }

       if(Input.GetMouseButtonDown(0)) //if click at enemy
       {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit, Mathf.Infinity, enemyLayerMask))
            {
                if(enemySelectionIndicator != null)
                {
                    enemySelectionIndicator.SetActive(false);
                    
                }
                soundEffectController = hit.collider.GetComponentInChildren<SoundEffectController>();
                soundEffectController.PlayBuildingOrUnitSelectionSound();
                isSelectEnemy = true;
            }

            else
            {
                if (enemySelectionIndicator != null)
                {
                    enemySelectionIndicator.SetActive(false);
                    isSelectEnemy = false;
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayerMask))
            {
                soundEffectController = hit.collider.GetComponentInChildren<SoundEffectController>();
                soundEffectController.PlayBuildingOrUnitSelectionSound();
            }
        }
    }

    private void CheckSelection()
    {
        if(unitSelection.unitSelected.Count == 1)
        {
            removeButton.SetActive(true);
            unitDetailsUI.SetActive(true); // show detail UI
                unitStat = unitSelection.unitSelected[0].GetComponent<UnitStat>();
                thisGameObject = unitSelection.unitSelected[0].gameObject;
                unitName.text = unitStat.unitName;
                unitAttackDetail.text = "Attack : " + unitStat.unitDamage.ToString();
                unitHPDetail.text = "HP : " + unitStat.unitHP.ToString();
                unitMeleeArmorDetail.text = "Melee Armor : " + unitStat.unitMeleeArmor.ToString();
                unitRangedArmorDetail.text = "Ranged Armor : " + unitStat.unitRangedArmor.ToString();
        }
    }

    private void SelectEnemy()
    {
        unitStat = hit.collider.GetComponent<UnitStat>();
        unitName.text = unitStat.unitName;
        unitAttackDetail.text = "Attack : " + unitStat.unitDamage.ToString();
        unitHPDetail.text = "HP : " + unitStat.unitHP.ToString();
        unitMeleeArmorDetail.text = "Melee Armor : " + unitStat.unitMeleeArmor.ToString();
        unitRangedArmorDetail.text = "Ranged Armor : " + unitStat.unitRangedArmor.ToString();
        enemySelectionIndicator = hit.transform.GetChild(0).gameObject;
        enemySelectionIndicator.SetActive(true);
        removeButton.SetActive(false);
        unitDetailsUI.SetActive(true);
    }

    public void DeleteUnit()
    {
        unitSelection.DeselectAll();

        #region Remove Unit
        if (thisGameObject.CompareTag("Villager"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[0].population);
            unitStat.unitHP = 0;
        }

        if (thisGameObject.CompareTag("Gunner"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[1].population);
            unitStat.unitHP = 0;
        }

        if (thisGameObject.CompareTag("Landsknecht"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[2].population);
            unitStat.unitHP = 0;
        }

        if (thisGameObject.CompareTag("Captain"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[3].population);
            unitStat.unitHP = 0;
        }

        if (thisGameObject.CompareTag("Kartouwe"))
        {
            population.PopulationChanges(-1 * unitDatabase.unitDetails[4].population);
            unitStat.unitHP = 0;
        }
        #endregion
    }
}
