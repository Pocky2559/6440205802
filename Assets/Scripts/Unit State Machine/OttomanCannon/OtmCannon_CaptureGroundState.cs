using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmCannon_CaptureGroundState : OtmCannonBaseState
{
    public override void EnterState(OtmCannonStateController otmCannon)
    {
        
    }

    public override void UpdateState(OtmCannonStateController otmCannon)
    {
        // Update
    }

    public override void OnTriggerStay(OtmCannonStateController otmCannon, Collider coll)
    {
        // Triggered Stay
    }

    public override void OnTriggerExit(OtmCannonStateController otmCannon, Collider coll)
    {
        // Triggered Ex
    }
}
