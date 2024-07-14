using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMedievalWallDestroyed : MonoBehaviour
{
    [SerializeField] private SoundEffectController soundEffectController;
    [SerializeField] private GateWallExplode explodeParticle;
    [SerializeField] private Transform pointToExplode;

    private void Awake()
    {
        explodeParticle = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<GateWallExplode>();
        soundEffectController = GetComponentInChildren<SoundEffectController>();
        pointToExplode = gameObject.transform;
    }

    public void StartToDestroyOldWall()
    {
        explodeParticle.StartPlayParticle(pointToExplode.position);
        StartToPlayDestroyedSound();
        Destroy(gameObject, 0.1f); //Destroy old medieval wall
        
    }

    private void StartToPlayDestroyedSound()
    {
        GameObject explodeSound = Instantiate(soundEffectController).gameObject;
        SoundEffectController soundEffect = explodeSound.GetComponent<SoundEffectController>();
        soundEffect.PlayDeleteBuildingSound();
        Destroy(explodeSound, 3f);
    }
}
