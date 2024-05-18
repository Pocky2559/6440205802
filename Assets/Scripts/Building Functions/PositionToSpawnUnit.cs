using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToSpawnUnit : MonoBehaviour
{
    [SerializeField] List<GameObject> positionToSpawn;
    public int roundNumber;
    private void Update()
    {
        if(roundNumber == 13)
        {
            roundNumber = 0;
        }

        for(int i = 0; i < positionToSpawn.Count; i++)
        {
            if(roundNumber == i)
            {
                gameObject.transform.position = positionToSpawn[i].transform.position;
            }
        }

        
    }
}
