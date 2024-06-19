using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballExplodeParticle : MonoBehaviour
{
    [SerializeField] private GameObject explodeParticle;

    public void StartPlayParticle(Vector3 positionToSpawn)
    {
        GameObject particle = Instantiate(explodeParticle, positionToSpawn, Quaternion.identity);
        Destroy(particle,4);
    }
}
