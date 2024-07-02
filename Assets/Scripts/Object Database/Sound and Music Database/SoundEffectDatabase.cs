using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class SoundEffectDatabase : ScriptableObject
{
    [Header("NPC Sound Effects")]
    public AudioClip gunFireSound;
    public AudioClip gatheringSound;
    public AudioClip playerSwordHitSound;
    public AudioClip enemySwordHitSound;
    public AudioClip storingSound;
    public AudioClip walkSound;
    
    public AudioClip unitDieSound;

    [Header("Placement Sound Effects")]
    public AudioClip placeBuildingSound;
    public AudioClip cannotPlaceSound;
    public AudioClip canPlaceBuildingSound;
    public AudioClip deleteBuildingSound;

    [Header("UI Interaction Sound Effect")]
    public AudioClip uiClickSound;
    public AudioClip uiCloseSound;

    [Header("World Sound Effects")]
    public AudioClip wallOrGateExplodeSound;
    public AudioClip gateOpenSound;
    public AudioClip gateCloseSound;
    public AudioClip hornSound;
    public AudioClip commandSound;
}
