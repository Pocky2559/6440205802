using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private SoundEffectDatabase soundEffectDatabase;

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

}
