using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed = 3;
    public float speed = 50f;
    //public float jumpPower;
    //public bool grounded;
    private Rigidbody2D rb2d;

    //private Rigidbody2D r;

    private Animator anim;
    private Vector3 input;

    // Use this for initialization
    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();

       //r = transform.GetComponent<Rigidbody2D>();

        anim = gameObject.GetComponent<Animator>();
        //jumpPower = 250f;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x + rb2d.velocity.y));
        anim.SetBool("Shooting", false);

        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-10f, 10f, 1f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(10f, 10f, 1f);
        }

        if (Input.GetKeyDown("f"))
        {
            anim.SetBool("Shooting", true);
        }

        /*if(Input.GetButtonDown("Jump") && grounded)
        {
            rb2d.AddForce(Vector2.up * jumpPower);
            grounded = false;
        }*/

    }

    //for physics
    void FixedUpdate()
    {
        Physics2D.gravity = Vector2.zero;

        //float h = Input.GetAxis("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");


        //rb2d.velocity = new Vector2 (h * speed, y * speed);
        //rb2d.AddForce((Vector2.up * speed) * y);

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
