using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed = 3;
    public float speed = 50f;
    //public float jumpPower;
    //public bool grounded;
    private Rigidbody2D rb2d;
    private Animator anim;

	// Use this for initialization
	void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        //jumpPower = 250f;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Shooting", false);

        if (Input.GetAxis("Horizontal") < -.01f)
        {
            transform.localScale = new Vector3(-10.68855f, 8.060138f, 1f);
        }

        if (Input.GetAxis("Horizontal") > .01f)
        {
            transform.localScale = new Vector3(10.68855f, 8.060138f, 1f);
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

    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");

        /*if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }*/

        rb2d.AddForce((Vector2.right * speed) * h);

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

    }
}
