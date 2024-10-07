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
    public AudioClip SwordHitSound;
    public AudioClip cannonFireSound;
    public AudioClip cannonballExplodeSound;
    public AudioClip storingSound;
    public AudioClip walkSound;  
    public AudioClip unitDieSound;

    [Header("Placement Sound Effects")]
    public AudioClip placeBuildingSound;
    public AudioClip cannotPlaceSound;
    public AudioClip deleteBuildingSound;

    [Header("UI Interaction Sound Effect")]
    public AudioClip uiClickSound;
    public AudioClip uiCloseSound;

    [Header("World Sound Effects")]
    public AudioClip gateSound;
    public AudioClip hornSound;
    public AudioClip commandSound;
    public AudioClip defendedPointAlarm;
    public AudioClip buildingOrUnitSelectionSound;
}
