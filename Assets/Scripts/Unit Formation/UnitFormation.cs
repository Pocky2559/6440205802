using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class UnitFormation : MonoBehaviour
{
    public UnitSelection unitSelection;
    public LayerMask wallLayerMask;
    public LayerMask blockingLayerMask;
    public NavMeshAgent unit;
    public List<Vector3> unitFormationPosition;
    
    [SerializeField] Vector3 leaderPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            #region Move on terrain

            if (unitSelection != null
                      && unitSelection.unitSelected.Count > 1
                       && Physics.Raycast(ray, out hit, Mathf.Infinity)
               )                 
            {
                  for (int i = 0; i < unitSelection.unitSelected.Count; i++) // for loop to check that in unitSelected List Is there any null object?
                  {
                     if (unitSelection.unitSelected[i] == null) // if it have
                     {
                            unitSelection.unitSelected.Remove(unitSelection.unitSelected[i]); // Remove that null object out from List to make the formation calculation work correctly
                     }
                  }  
                  Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 5f);
                if (hit.collider.CompareTag("Ground")
                    && !hit.collider.CompareTag("Wood")
                    && !hit.collider.CompareTag("Food")
                    && !hit.collider.CompareTag("Gold")
                    && !hit.collider.CompareTag("Stone")
                    && !hit.collider.CompareTag("Blocking"))
                  {
                        leaderPosition = hit.point;

                        for (int i = 0; i < unitSelection.unitSelected.Count; i++)
                        {
                            if (i < 5)
                            {
                                unitFormationPosition.Add(leaderPosition + new Vector3(i, 0, 0));
                            }

                            else if (i >= 5)
                            {
                                int row = i / 5;
                                int col = i % 5;
                                unitFormationPosition.Add(leaderPosition + new Vector3(col, 0, row));
                            }
                        }
                        MoveFormationUnit();
                  }
                
                #endregion       
            }    

            #region Move on wall
            if (unitSelection != null
                && unitSelection.unitSelected.Count > 1
                && Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayerMask))
            {
                leaderPosition = hit.point;

                for (int i = 0; i < unitSelection.unitSelected.Count; i++)
                {
                  unitFormationPosition.Add(leaderPosition + new Vector3(i, 0, 0));
                }
              MoveFormationUnit();
            }
            #endregion

            #region Single Selection Move on any terrian
            if (unitSelection != null
                && unitSelection.unitSelected.Count == 1
                && Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Ground") 
                    && !hit.collider.CompareTag("Wood")
                    && !hit.collider.CompareTag("Food")
                    && !hit.collider.CompareTag("Gold")
                    && !hit.collider.CompareTag("Stone")
                    && unitSelection.unitList.Contains(unitSelection.unitSelected[0]))
                {
                    unit = unitSelection.unitSelected[0].GetComponent<NavMeshAgent>();
                    unit.enabled = true;
                    unit.isStopped = false;
                    unit.SetDestination(hit.point);
                }
            }
            if (unitSelection != null
                && unitSelection.unitSelected.Count == 1
                && Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayerMask))
            {
                unit = unitSelection.unitSelected[0].GetComponent<NavMeshAgent>();
                unit.enabled = true;
                unit.isStopped = false;
                unit.SetDestination(hit.point);
            }
            #endregion

        }
    }

    public void MoveFormationUnit()
    {
        for (int i = 0; i < unitSelection.unitSelected.Count; i++)
        { 
            try
            {
                unit = unitSelection.unitSelected[i].GetComponent<NavMeshAgent>();
                unit.enabled = true; //make NavMeshAgent active
                unit.isStopped = false;
                unit.SetDestination(unitFormationPosition[i]);
            }
            catch
            {
                Debug.Log("Error");
            }
        }
        unitFormationPosition.Clear();
    }
}
