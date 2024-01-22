using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsAgentReach : MonoBehaviour
{
    Transform currentObject;
    Transform targetEnemyObject;
    private NavMeshAgent myAgent;
    bool IsEnemyAlive = true;

    private void Awake()
    {
        myAgent= GetComponent<NavMeshAgent>();
    }
    public void AssignValueInIsAgentReach(Transform objectPosition, Transform targetEnemy )
    {
        currentObject= objectPosition;
        targetEnemyObject= targetEnemy;
    }
   public void IsAgentReachTarget() //checking Is agent reach the target? If reached stop moving and firing.
    {
        if (targetEnemyObject != null)
        {
            float distance = Vector3.Distance(currentObject.position, targetEnemyObject.position);

            // Check if the enemy is both within the range and still alive
            if (distance <= 9.8f)
            {
                myAgent.isStopped = true;

            }
            if (distance > 9.8f && IsEnemyAlive == true) // chasing
            {
                myAgent.isStopped = false;
                myAgent.SetDestination(targetEnemyObject.position);
            }
        }
    }

    public void Update()
    {
        IsAgentReachTarget();
    }
}
