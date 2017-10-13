﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    private GameObject Player;
    public float speed = 1f;
    public float maxSpeed = .01f;
    public float health;
    public bool idle;
    public float enemyCount = 10f;
    private bool activateEnemy = false;
    public bool enemyShooting;
    private float randomIndex;
    private int itemIndex;
    private Vector3 enposition;

    private Animator anim;
    private Rigidbody2D rb2d;
    private float minDistance = .524848f;
    private float range;
    public float enemyDamage;
    public float enemyMadness;
    RaycastHit2D hit;
    Player controlscript;
    bool contact;

    public GameObject[] items;
    public GameObject mainCamera;
    public Camera camera;
    public Transform spawnPoint;
    public GameObject bullet;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        controlscript = Player.GetComponent<Player>();
        mainCamera = GameObject.FindWithTag("MainCamera");
        camera = mainCamera.GetComponent<Camera>();
        health = 300f;
        enemyDamage = 25f;
        enemyMadness = 10f;
        contact = false;
        idle = true;
        anim.SetBool("Idle", true);

    }

    void Update()
	{
        enemyShooting = false;

		if (controlscript.dead)
		{
			anim.SetBool("Idle", true);
			idle = true;
			anim.SetFloat("Speed", 0f);
		}
		else {
			range = Vector2.Distance (transform.position, Player.transform.position);
		}

        Vector3 viewPos = camera.WorldToViewportPoint(gameObject.transform.position);

		if (viewPos.x > 0 && viewPos.x < 1 && viewPos.y >0 && viewPos.y <1 && viewPos.z > 0 && !controlscript.dead) //activate enemy
        {
			anim.SetBool("Idle", false);
			idle = false;
			anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x) + Mathf.Abs(rb2d.velocity.y));
        }

        if (gameObject.tag == "Enemy")
        {
            if (range <= minDistance && !idle)
            {
                rb2d.isKinematic = true;
                anim.SetBool("Attack", true);
            }

            if (range > minDistance && !idle)
            {
                rb2d.isKinematic = false;
                anim.SetBool("Attack", false);

                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                if (Player.transform.position.x > transform.position.x)
                {
                    //face right
                    transform.localScale = new Vector3(1.5f, 1.5f, 1);
                }
                else if (Player.transform.position.x < transform.position.x)
                {
                    //face left
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                }
            }
        }

        if (gameObject.tag == "RangeEnemy")
        {

            if (range <= 3f && !idle && Player.transform.position.y >= gameObject.transform.position.y - .3f && Player.transform.position.y <= gameObject.transform.position.y + .3f)
            {
                rb2d.isKinematic = true;
                if (Player.transform.position.x > transform.position.x)
                {
                    //face right
                    transform.localScale = new Vector3(1.5f, 1.5f, 1);
                }
                else if (Player.transform.position.x < transform.position.x)
                {
                    //face left
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                }
                anim.SetBool("Attack", true);
            }
            else if (!idle)
            {
                rb2d.isKinematic = false;
                anim.SetBool("Attack", false);

                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                if (Player.transform.position.x > transform.position.x)
                {
                    //face right
                    transform.localScale = new Vector3(1.5f, 1.5f, 1);
                }
                else if (Player.transform.position.x < transform.position.x)
                {
                    //face left
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                }
            }
        }
    }

    void FixedUpdate()
    {

        Physics2D.gravity = Vector2.zero;

        if (idle == true)
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
        }

        else
        {
            rb2d.velocity = (Player.transform.position - transform.position).normalized * speed;
        }

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.y > maxSpeed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxSpeed);
        }

        if (rb2d.velocity.y < -maxSpeed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -maxSpeed);
        }
    }

    void Damage()
    {

        if (Player.transform.position.y >= gameObject.transform.position.y - .3f && Player.transform.position.y <= gameObject.transform.position.y + .3f)
        {
            Player.GetComponent<Player>().playerHealth -= enemyDamage;
            Player.GetComponent<Player>().playerMadness += enemyMadness;
            Player.GetComponent<Animator>().SetBool("Hit", true);
        }
    }

    void SetHit()
    {
        gameObject.GetComponent<Animator>().SetBool("Hit", false);
    }

    private void Destroy()
    {
        enposition = gameObject.GetComponent<EnemyMove>().transform.position;
        randomIndex = Random.Range(1f, 100f);
        if (randomIndex <= 60f && randomIndex >= 1f)
        {
            //itemIndex = Random.Range(0, collision.gameObject.GetComponent<EnemyMove>().items.Length - 1);
            itemIndex = Random.Range(3, gameObject.GetComponent<EnemyMove>().items.Length);
            GameObject coin = Instantiate(gameObject.GetComponent<EnemyMove>().items[itemIndex], enposition, Quaternion.identity);
            Destroy(coin, 5);
        }

        else
        {
            itemIndex = Random.Range(0, gameObject.GetComponent<EnemyMove>().items.Length - 2);
            GameObject coin = Instantiate(gameObject.GetComponent<EnemyMove>().items[itemIndex], enposition, Quaternion.identity);
            Destroy(coin, 5);
        }
        Destroy(gameObject);
        Player.GetComponent<Player>().enemiesKilled++;
    }

    void InstantiateBullet()
    {
        Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        //Transform newBullet = Instantiate(bullet.transform, spawnPoint.position, Quaternion.identity) as Transform;
        //newBullet.parent = transform;
        enemyShooting = true;
    }
}
