using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public Transform Player;
    private Animator anim;
    private Rigidbody2D rb2d;
    public float speed = 1f;
    public float maxSpeed = .001f;
    private float minDistance = 0.2f;
    private float range;


     void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Physics2D.gravity = Vector2.zero;
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x + rb2d.velocity.y));
        Player = GameObject.FindWithTag("Player").transform;
        range = Vector2.Distance(transform.position, Player.position);
        if (range > minDistance)
        {
            //Debug.Log(range);
            Player = GameObject.FindWithTag("Player").transform;
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        Player = GameObject.FindWithTag("Player").transform;
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

}
