using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public List<GameObject> unitList = new();
    public List<GameObject> unitSelected = new();
    private static UnitSelection _Instance;
    public static UnitSelection Instance { get { return _Instance; } }

    private void Awake()
    {
        //if an instance of this already exists and it isn't this one
        if(_Instance != null && _Instance != this)
        {
            //we destroy this instance
            Destroy(this.gameObject);
        }
        else 
        { 
            //make this instance
            _Instance = this;
        }
    }
    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitSelected.Add(unitToAdd);
        unitToAdd.transform.GetChild(0).gameObject.SetActive(true); //Show selection indicator
       // unitToAdd.GetComponent<UnitMovement>().enabled= true;
    }
    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if(!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
           // unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
        else
        {
            //unitToAdd.GetComponent<UnitMovement>().enabled = false;
            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
            unitSelected.Remove(unitToAdd);
        }
    }
    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
           // unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
    }
    public void DeselectAll()
    {
        try 
        {
            foreach (var unit in unitSelected)
            {
                //unit.GetComponent<UnitMovement>().enabled = false;
                unit.transform.GetChild(0).gameObject.SetActive(false);
            }
            unitSelected.Clear();
        }
        catch // if selected object was destroyed and player try to deselected it 
        {
            unitSelected.Clear(); //Clear all selected units
        }
    }
    public void Deselect(GameObject unitToDeselect)
    {

    }
}
    