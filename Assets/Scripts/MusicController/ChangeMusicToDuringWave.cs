using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicToDuringWave : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;

    [Header("Audio Source")]
    [SerializeField] private AudioSource preWaveMusic;
    [SerializeField] private AudioSource duringWaveMusic;

    [SerializeField] private bool isPreWaveMusicStartFade;

    private void Start()
    {
        duringWaveMusic.volume = 0;
    }

    private void Update()
    {
        if(isPreWaveMusicStartFade == true && preWaveMusic.volume > 0)
        {
            preWaveMusic.volume -= fadeSpeed * Time.deltaTime;
            duringWaveMusic.volume += fadeSpeed * Time.deltaTime;
        }
    }

    public void TransitionMusicToDuringWave()
    {
        isPreWaveMusicStartFade = true;
        duringWaveMusic.Play();
    }
}
