using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallFunction : MonoBehaviour // Working with ExplosiveArea script
{
    public GameObject cannonball;
    public Vector3 targetPosition;
    private bool isCannonballFiring;
    public float speed = 50f;
    public ExplosiveArea explosiveArea;

    public void AssignValueOfCannonball(GameObject ball, Vector3 enemyPosition)
    {   
        cannonball = ball;
        targetPosition = enemyPosition;
        isCannonballFiring = true;
    }
   
    private void Update()
    {
       if(isCannonballFiring == true)
       {
          MoveCannonball();
       }
    }

    private void MoveCannonball()
    {
        Vector3 direction = (targetPosition - cannonball.transform.position).normalized;
        cannonball.transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            explosiveArea.enabled = true;
            Destroy(this.gameObject, 0.1f);
        }
    }
}
