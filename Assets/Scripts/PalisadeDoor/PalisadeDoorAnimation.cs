using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeDoorAnimation : MonoBehaviour
{
    public Animator leftDoorAnimator;
    public Animator rightDoorAnimator;

    public void LeftDoorOpen()
    {
        leftDoorAnimator.SetTrigger("isOpen");
        leftDoorAnimator.ResetTrigger("isClose");

        rightDoorAnimator.SetTrigger("isOpen");
        rightDoorAnimator.ResetTrigger("isClose");
    }

    public void LeftDoorClose()
    {
        leftDoorAnimator.SetTrigger("isClose");
        leftDoorAnimator.ResetTrigger("isOpen");

        rightDoorAnimator.SetTrigger("isClose");
        rightDoorAnimator.ResetTrigger("isOpen");
    }
}
