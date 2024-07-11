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
    [SerializeField] private SoundEffectController soundController;

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
        if (other.CompareTag("OttomanRecruit")
           || other.CompareTag("OttomanGunnerRecruit")
           || other.CompareTag("MeleeJanissary")
           || other.CompareTag("RangedJanissary")
           || other.CompareTag("OttomanCannon")
           )
        {
            explosiveArea.enabled = true;
            explodeParticle.StartPlayParticle(this.transform.position);
            GameObject explodeSound = Instantiate(soundController.gameObject);
            SoundEffectController soundEffectController = explodeSound.GetComponent<SoundEffectController>();
            AudioSource audioSource = soundEffectController.GetComponent<AudioSource>();
            audioSource.enabled = true;
            soundEffectController.PlayCannonballExplodeSound();
            
            Destroy(explodeSound,3);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject,2f);
        }
    }
}
