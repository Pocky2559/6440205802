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
    public CannonballExplodeParticle explodeParticle;
    [SerializeField] private SoundEffectController soundEffectController;

    private void Awake()
    {
        explodeParticle = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<CannonballExplodeParticle>();
    }

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
            || other.CompareTag("PalisadeGate"))
        {
            explosiveArea.enabled = true;
            explodeParticle.StartPlayParticle(this.transform.position);
            GameObject explodeSound = Instantiate(soundEffectController.gameObject);
            SoundEffectController soundEffect = explodeSound.GetComponent<SoundEffectController>();
            AudioSource audioSource = soundEffect.GetComponent<AudioSource>();
            audioSource.enabled = true;
            soundEffect.PlayCannonballExplodeSound();

            Destroy(explodeSound,3f);
            Destroy(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject, 2f);
        }
    }
}
