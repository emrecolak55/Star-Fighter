using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 1200;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathVolume = 0.75f;
    [Header("For Second Player")]
    [SerializeField] float laser_starting_pos = 0.5f;
    [Header("Projectile")]
    [SerializeField] GameObject laser_prefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserVolume = 0.25f;

    Quaternion myrotation;
    Coroutine firingCoroutine;
    float xMin, xMax,yMin ,yMax;
    // Start is called before the first frame update
    void Start()
    {
        FireTwo(); // sil
        //myrotation = new Quaternion();
        //myrotation.Set(0, 0, 2.5f/4,1);
        SetUpMoveBoundaries();
    }

   

    // Update is called once per frame
    void Update()
    {
        
        Move();
        /*
        if(gameObject.tag == "SecondPlayer")
        {
            FireTwo();
        }
        else
        {
            Fire();
        }
        */
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
        Destroy(gameObject);
        
    }

    public int GetHealth()
    {
        return health;
    }

    private IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(laser_prefab, transform.position, Quaternion.identity) as GameObject; // ? ? ? ? 
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    private IEnumerator FireTwoLasers()
    {
        while (true)
        {
            Vector3 l1vec = new Vector3(transform.position.x - laser_starting_pos, transform.position.y, transform.position.z);
            Vector3 l2vec = new Vector3(transform.position.x + laser_starting_pos, transform.position.y, transform.position.z);
            GameObject laser1 = Instantiate(laser_prefab, l1vec, Quaternion.identity) as GameObject; // ? ? ? ? 
            GameObject laser2 = Instantiate(laser_prefab, l2vec, Quaternion.identity) as GameObject;
            laser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, projectileSpeed);
            laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    private void Fire()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
            
    }
    private void FireTwo()
    {
        /*
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireTwoLasers());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
        */
        
            firingCoroutine = StartCoroutine(FireTwoLasers());
        
        

    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXpos = Mathf.Clamp( transform.position.x + deltaX , xMin, xMax);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin , yMax );
        transform.position = new Vector2(newXpos, newYpos);

        
    }
    private void MoveVertical()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYpos = transform.position.y + deltaY;
        transform.position = new Vector2(transform.position.x , newYpos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
