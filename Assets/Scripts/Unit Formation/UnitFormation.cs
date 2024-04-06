using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitFormation : MonoBehaviour
{
    public UnitSelection unitSelection;
    public LayerMask terrainLayerMask;
    public LayerMask wallLayerMask;
    public NavMeshAgent unit;
    public List<Vector3> unitFormationPosition;
    private Ray ray;
    private RaycastHit hit;
    [SerializeField] Vector3 leaderPosition;

    void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Formation Ray");

            #region Move on terrain

            //Multiple Selection
            if (unitSelection.unitSelected.Count > 1 && Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayerMask))
            {
                leaderPosition = hit.point;

                for (int i = 0; i < unitSelection.unitSelected.Count; i++)
                {
                    if(i < 5)
                    {
                        unitFormationPosition.Add(leaderPosition + new Vector3(i, 0, 0));
                    }

                    else if(i >= 5)
                    {
                        int row = i / 5;
                        int col = i % 5;
                        unitFormationPosition.Add(leaderPosition + new Vector3(col, 0, row));
                    }
                }
                    MoveFormationUnit();
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
            if (unitSelection.unitSelected.Count == 1 && Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayerMask))
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
        unitFormationPosition.Clear();



    }
}
