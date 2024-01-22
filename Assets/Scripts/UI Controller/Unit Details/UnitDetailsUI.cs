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
    public UnitStat unitStat;

    public TMP_Text unitName;
    public TMP_Text unitAttackDetail;
    public TMP_Text unitHPDetail;
    public TMP_Text unitMeleeArmorDetail;
    public TMP_Text unitRangedArmorDetail;

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
        Destroy(unitStat.gameObject);
    }
}
