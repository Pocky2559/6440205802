using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeDoorDetector : MonoBehaviour
{
    public PalisadeDoorAnimation palisadeDoorAnimation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villager")
            || other.CompareTag("Gunner")
            || other.CompareTag("Landsknecht")
            || other.CompareTag("Captain"))
        {
            palisadeDoorAnimation.LeftDoorOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        palisadeDoorAnimation.LeftDoorClose();
    }
}
