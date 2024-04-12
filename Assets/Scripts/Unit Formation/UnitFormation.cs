using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitFormation : MonoBehaviour
{
    public UnitSelection unitSelection;
    public LayerMask wallLayerMask;
    public NavMeshAgent unit;
    public List<Vector3> unitFormationPosition;
    
    [SerializeField] Vector3 leaderPosition;

    void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            #region Move on terrain

            //Multiple Selection
            if (unitSelection.unitSelected.Count > 1
                && Physics.Raycast(ray, out hit, Mathf.Infinity)
               )
            {
                Debug.Log("TerrainRay hits " + hit.collider.name);
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 5f);
                if (hit.collider.CompareTag("Ground")
                    && !hit.collider.CompareTag("Wood")
                    && !hit.collider.CompareTag("Food")
                    && !hit.collider.CompareTag("Gold")
                    && !hit.collider.CompareTag("Stone"))
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
            }
                #endregion

                #region Move on wall
                if (unitSelection.unitSelected.Count > 1 && Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayerMask))
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
            if (unitSelection.unitSelected.Count == 1
                && Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Ground") 
                    && !hit.collider.CompareTag("Wood")
                    && !hit.collider.CompareTag("Food")
                    && !hit.collider.CompareTag("Gold")
                    && !hit.collider.CompareTag("Stone"))
                {
                    unit = unitSelection.unitSelected[0].GetComponent<NavMeshAgent>();
                    unit.SetDestination(hit.point);
                }
            }
            if (unitSelection.unitSelected.Count == 1 && Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayerMask))
            {
                unit = unitSelection.unitSelected[0].GetComponent<NavMeshAgent>();
                unit.SetDestination(hit.point);
            }
            #endregion

        }
    }

    public void MoveFormationUnit()
    {
        for (int i = 0; i < unitSelection.unitSelected.Count; i++)
        {
            unit = unitSelection.unitSelected[i].GetComponent<NavMeshAgent>();
            unit.SetDestination(unitFormationPosition[i]); 
        }
        Debug.Log("Unit Formation");
        unitFormationPosition.Clear();
    }
}
