using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public Transform Player;
    public float speed = 1f;
    public float maxSpeed = .01f;
    public float health;
	public bool idle;

    private Animator anim;
    private Rigidbody2D rb2d;
    private float minDistance = 1.25f;
    private float range;
    public float enemyDamage;
	public float enemyMadness;
    RaycastHit2D hit;
    Player controlscript;
    bool contact;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player").transform;
        controlscript = Player.GetComponent<Player>();
        health = 200f;
        enemyDamage = 25f;
		enemyMadness = 10f;
        contact = false;
		idle = true;
        
    }

    void Update()
	{
		
		if (controlscript.playerHealth > 0 && controlscript.playerMadness < 100 ) {
			anim.SetBool ("Idle", false);
			idle = false;
		} 
		else {
			anim.SetBool ("Idle", true);
			idle = true;
		}
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x) + Mathf.Abs(rb2d.velocity.y));
        range = Vector2.Distance(transform.position, Player.position);




		if (range <= minDistance && !idle)
        {
            rb2d.isKinematic = true;
            anim.SetBool("Attack", true);
        }


        if (range > minDistance && !idle)
        {
            rb2d.isKinematic = false;
            anim.SetBool("Attack", false);

            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
            if (Player.position.x > transform.position.x)
            {
                //face right
                transform.localScale = new Vector3(4, 4, 1);
            }
            else if (Player.position.x < transform.position.x)
            {
                //face left
                transform.localScale = new Vector3(-4, 4, 1);
            }
        }
    }

    void FixedUpdate()
    {

        Physics2D.gravity = Vector2.zero;

        rb2d.velocity = (Player.position - transform.position).normalized * speed;

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
        if (anim.GetBool("Attack") == true)
        {
            if (Player.position.x > transform.position.x)
            {
                hit = Physics2D.Raycast(transform.position, Vector2.right);
            }

            else
            {
                hit = Physics2D.Raycast(transform.position, Vector2.left);
            }

            if (hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Player>().playerHealth -= enemyDamage;
				hit.collider.gameObject.GetComponent<Player>().playerMadness += enemyMadness;
                //if (hit.collider.gameObject.GetComponent<Player>().playerHealth <= 0f)
                //{
                    //Destroy(Player.gameObject);
                //}

            }
            anim.SetBool("Attack", false);
        }

    }

}
