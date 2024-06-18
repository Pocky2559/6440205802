using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmsParticle : MonoBehaviour
{
    [SerializeField] private GameObject firearmsParticle;

    public void StartPlayParticle(Vector3 particleOriginPosition)
    {
        GameObject particle = Instantiate(firearmsParticle, particleOriginPosition, Quaternion.identity);
        Destroy(particle,5);
    }
}
