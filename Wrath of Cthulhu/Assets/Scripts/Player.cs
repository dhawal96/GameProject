using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed = 2;
    public float speed = 50f;
    public float playerHealth;

    public GameObject bullet;
    public Transform spawnPoint;

    private Animator anim;
    private Vector3 input;
    private Rigidbody2D rb2d;
    private bool shootOnce;

    // Use this for initialization
    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        playerHealth = 100;
       
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x + rb2d.velocity.y));
        anim.SetBool("Shooting", false);

        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-5f, 5f, 1f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(5f, 5f, 1f);
        }

        if (Input.GetKeyDown("f") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark"))
        {
            anim.SetBool("Shooting", true);
            shootOnce = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark") && shootOnce)
        {
            Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            shootOnce = false;
        }


    }

    //for physics
    void FixedUpdate()
    {
        Physics2D.gravity = Vector2.zero;


        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddForce(Vector3.left * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(Vector3.right * speed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb2d.AddForce(Vector3.up * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb2d.AddForce(Vector3.down * speed);
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
}
