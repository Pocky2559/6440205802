using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeDoorDetector : MonoBehaviour
{
    public PalisadeDoorAnimation palisadeDoorAnimation;
    [SerializeField] private SoundEffectController soundEffectController;
    private bool isDoorOpen;

    private void Awake()
    {
        soundEffectController = GetComponentInChildren<SoundEffectController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villager")
            || other.CompareTag("Gunner")
            || other.CompareTag("Landsknecht")
            || other.CompareTag("Captain"))
        {
            palisadeDoorAnimation.LeftDoorOpen();
            if(isDoorOpen == false)
            {
                soundEffectController.PlayGateOpenSound();
                isDoorOpen = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        palisadeDoorAnimation.LeftDoorClose();
        if(isDoorOpen == true)
        {
            soundEffectController.PlayGateOpenSound();
            isDoorOpen = false;
        }
    }
}
