using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [Header("Fading Speed")]
    [SerializeField] private float fadeSpeed1;
    [SerializeField] private float fadeSpeed2;

    [Header("Audio Source")]
    [SerializeField] private AudioSource preWaveMusic;
    [SerializeField] private AudioSource duringWaveMusic;
    [SerializeField] private AudioSource victoryTheme;
    [SerializeField] private AudioSource loseTheme;

    [Header("Booleans")]
    [SerializeField] private bool isPreWaveMusicStartFade;
    [SerializeField] private bool isWin;
    [SerializeField] private bool isLose;

    private void Update()
    {
        if(isPreWaveMusicStartFade == true && duringWaveMusic.volume <= 0.07)
        {
            preWaveMusic.volume -= (fadeSpeed1 + 0.1f) * Time.deltaTime;
            duringWaveMusic.volume += fadeSpeed1 * Time.deltaTime;
        }

        if(isWin == true && victoryTheme.volume <= 0.5)
        {
            duringWaveMusic.volume -= (fadeSpeed2 + 0.1f) * Time.deltaTime;
            victoryTheme.volume += fadeSpeed2 * Time.deltaTime;
        }

        if(isLose == true && loseTheme.volume <= 1)
        {
            duringWaveMusic.volume -= (fadeSpeed2 + 0.1f) * Time.deltaTime;
            loseTheme.volume += (fadeSpeed2 + 0.1f) * Time.deltaTime;
        }
    }

    public void TransitionMusicToDuringWave()
    {
        isPreWaveMusicStartFade = true;
        duringWaveMusic.Play();
    }

    public void TransitionMusicToVictoryTheme()
    {
        isWin = true;
        victoryTheme.Play();
    }

    public void TransitionMusicToLoseTheme()
    {
        isLose = true;
        loseTheme.Play();
    }
}
