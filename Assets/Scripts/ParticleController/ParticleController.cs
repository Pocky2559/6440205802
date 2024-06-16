using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Particle Effects")]
    [SerializeField] private GameObject gunShootParticle;
    [SerializeField] private GameObject wallDestroyParticle;
    [SerializeField] private GameObject gateDestroyParticle;
    [SerializeField] private GameObject swordHitParticle;
    [SerializeField] private GameObject cannonBallExplosionParticle;

    [Header("Particle List")]
    [SerializeField] private Dictionary<Transform, GameObject> allGunShootParticle;

    public void MakeGunShootParticle(Transform positionToSpawn)
    {
        allGunShootParticle.Add(positionToSpawn, gunShootParticle);
        StartCoroutine(DeleteGunShootParticle(positionToSpawn));
    }

    IEnumerator DeleteGunShootParticle(Transform positionToSpawn)
    {
        yield return new WaitForSeconds(0.5f);
        allGunShootParticle.Remove(positionToSpawn);

    }
}
