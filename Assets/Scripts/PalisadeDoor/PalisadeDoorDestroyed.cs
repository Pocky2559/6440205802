using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeDoorDestroyed : MonoBehaviour
{
    [SerializeField] private UnitStat unitStat;
    [SerializeField] private GateWallExplode gateWallExplode;
    [SerializeField] private SoundEffectController soundEffectController;

    private void Awake()
    {
        unitStat = GameObject.FindGameObjectWithTag("PalisadeGate").GetComponent<UnitStat>();
        gateWallExplode = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<GateWallExplode>();
        soundEffectController = GetComponentInChildren<SoundEffectController>();
    }
    private void Update()
    {
       if(unitStat.unitHP <= 0)
       {
          gateWallExplode.StartPlayParticle(gameObject.transform.position);
          StartToPlayGateDestroyedSound();
          Destroy(unitStat.transform.gameObject);
       }    
    }

    private void StartToPlayGateDestroyedSound()
    {
        GameObject explodeSound = Instantiate(soundEffectController).gameObject;
        SoundEffectController soundEffect = explodeSound.GetComponent<SoundEffectController>();
        soundEffect.PlayDeleteBuildingSound();
        Destroy(explodeSound.gameObject,3);
    }
}
