using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Accessibility;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.Experimental;
using System;


public class MovementAndAnimation : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    [SerializeField] private float movementSpeed = 3.0f;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    public Transform firePoint;
    private Vector2 movement;
    private Animator animator;
    public float Health;
    Vector2 mousePosition;
    public GameObject gameOverMenu;
    [SerializeField] float playerHealth, maxHealth = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Health < 1)
        {
            Health = 0;
            Debug.Log("RIP");
            gameOverMenu.SetActive(true);
        }
       

        movement = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")).normalized;
        animator.SetFloat("Speed", Mathf.Abs(movement.magnitude * movementSpeed));

        bool flipped = movement.x > 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

        if(Input.GetAxis("Jump") != 0)
        {
            gameOverMenu.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

             if (movement != Vector2.zero)
             {
                  var yMovement = movement.y * movementSpeed * Time.deltaTime;
                  var xMovement = movement.x * movementSpeed * Time.deltaTime;
                  this.transform.Translate(new Vector3(xMovement, yMovement), Space.World);
            }
        
         if ((shootHor != 0 || shootVert != 0) && (Time.time > lastFire + fireDelay))
         {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

         

        
    }
    public void TakeDamage(float damageAmount)
    {
        playerHealth -= damageAmount;

        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position , Quaternion.identity) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
            );
        bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(y, x) * Mathf.Rad2Deg);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health = Health - 1;
        }
    }



}
