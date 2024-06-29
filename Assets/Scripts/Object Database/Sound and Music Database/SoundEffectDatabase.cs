using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class SoundEffectDatabase : ScriptableObject
{
    [Header("Music Database")]
    public AudioClip mainMenuTheme;
    public AudioClip preWaveMusic;
    public AudioClip duringWaveMusic;
    public AudioClip winTheme;
    public AudioClip loseTheme;
}
