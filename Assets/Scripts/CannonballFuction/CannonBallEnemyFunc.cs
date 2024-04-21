using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallEnemyFunc : MonoBehaviour
{
    public GameObject cannonball;
    public Vector3 targetPosition;
    private bool isCannonballFiring;
    public float speed = 50f;
    public ExplosiveAreaEnemyCannonball explosiveArea;

    public void AssignValueOfCannonball(GameObject ball, Vector3 playerUnitPosition)
    {
        cannonball = ball;
        targetPosition = playerUnitPosition;
        isCannonballFiring = true;
        explosiveArea.enabled = false;
    }

    private void Update()
    {
        if (isCannonballFiring == true)
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
        if (other.CompareTag("Villager")
            || other.CompareTag("Landsknecht")
            || other.CompareTag("Gunner")
            || other.CompareTag("Captain")
            || other.CompareTag("Kartouwe")
            //|| other.CompareTag("Ground")
            || other.CompareTag("PalisadeGate"))
        {
            explosiveArea.enabled = true;
            Destroy(this.gameObject, 0.1f);
        }
        else
        {
            Destroy(this.gameObject, 3f);
        }
    }
}
