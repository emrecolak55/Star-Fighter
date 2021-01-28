using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.3f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectile_speed = 4f;
    [Header("Explosion")]
    [SerializeField] GameObject particle_effect_vfx;
    [SerializeField] float destroy_delay = 0.3f;
    [Header("Enemy Death Sound")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathVolume = 0.75f;
    [Header("Enemy Laser Sound")]
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserVolume = 0.25f;
    [Header("Game Session Part")]
    [SerializeField] int score_per_kill = 100;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
 
        gameSession = FindObjectOfType<GameSession>();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShot();
    }

    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }
    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject ;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectile_speed);
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVolume);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound , Camera.main.transform.position , deathVolume);

        GameObject explosion_vfx = Instantiate(particle_effect_vfx, transform.position, transform.rotation);
        Destroy(gameObject);
        gameSession.AddToScore(score_per_kill);
        Destroy(explosion_vfx, destroy_delay); // !!!!!!!!!!!!!!!!!!
    }
}
