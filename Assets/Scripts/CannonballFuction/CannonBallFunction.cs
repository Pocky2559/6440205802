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
    public CannonballExplodeParticle explodeParticle;

    private void Awake()
    {
        explodeParticle = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<CannonballExplodeParticle>();
    }

    public void AssignValueOfCannonball(GameObject ball, Vector3 enemyPosition)
    {   
        cannonball = ball;
        targetPosition = enemyPosition;
        isCannonballFiring = true;
        explosiveArea.enabled = false;
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
            explodeParticle.StartPlayParticle(gameObject.transform.position);
        }
        else
        {
            Destroy(this.gameObject, 3f);
        }
    }
}
