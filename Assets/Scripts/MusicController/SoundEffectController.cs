using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    public AudioSource soundEffect;
    public SoundEffectDatabase soundEffectDatabase;

    public void StopPlaySound()
    {
        soundEffect.Stop();
    }

    public void PlayGunFireSound()
    {
        soundEffect.clip = soundEffectDatabase.gunFireSound;
        soundEffect.loop = false;
        soundEffect.Play();
    }

    public void PlayPlayerSwordHitSound()
    {
        soundEffect.clip = soundEffectDatabase.SwordHitSound; 
        soundEffect.loop = false;
        soundEffect.Play();
    }

    public void PlayWalkingSound()
    {
        soundEffect.clip = soundEffectDatabase.walkSound;
        soundEffect.loop = true;
        soundEffect.Play();
    }

    public void PlayUnitDiedSound()
    {
        soundEffect.clip = soundEffectDatabase.unitDieSound;
        soundEffect.loop = false;
        soundEffect.Play();
    }

    public void PlayGatheringSound()
    {
        soundEffect.clip = soundEffectDatabase.gatheringSound;
        soundEffect.loop = true;
        soundEffect.Play();
    }

    public void PlayStoringSound()
    {
        soundEffect.clip = soundEffectDatabase.storingSound;
        soundEffect.loop = false;
        soundEffect.Play();
    }
}
