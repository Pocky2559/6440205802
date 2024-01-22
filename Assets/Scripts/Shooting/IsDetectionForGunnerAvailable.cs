using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsDetectionForGunnerAvailable : MonoBehaviour
{
    NavMeshAgent myAgent;
    DetectionForAllied detectionForAllied;
    public ShootRay shootRay;
    

    private void Awake()
    {
        myAgent = GetComponentInParent<NavMeshAgent>();
        detectionForAllied = GetComponent<DetectionForAllied>();
    }
    private void Update()
    {
        
        if(myAgent.velocity.magnitude >= 0.1f) // if agent is moving we don't want it to aim at enemy
        {
            detectionForAllied.enabled= false;
        }
        if(myAgent.remainingDistance <= myAgent.stoppingDistance && !myAgent.pathPending) // if agent is stop moving we need it look around for enemy
        {
            detectionForAllied.enabled= true;
        }
        if(myAgent.isStopped == true) // if the agent is stop suddenly it will look around for enemy
        {
            detectionForAllied.enabled= true;
        }
    }
}
