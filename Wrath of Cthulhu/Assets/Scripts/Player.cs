using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed = 2;
    public float speed = 50f;

	//Player1Health
    public float playerHealth;
	public Transform HealthPercentage;
	Player1Health healthscript;

	//Player1Madness
	public float playerMadness;
	public Transform MadnessPercentage;
	Player1Madness madnessscript;

	//Player1Bullet
    public GameObject bullet;
    public Transform spawnPoint;
    private AudioSource winchester;

    private Animator anim;
    private Vector3 input;
    private Rigidbody2D rb2d;
    private bool shootOnce;


    // Use this for initialization
    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        winchester = gameObject.GetComponent<AudioSource>();
        playerHealth = 100f;
		playerMadness = 0f;
		HealthPercentage = GameObject.Find("Player1Health").transform;
		healthscript = HealthPercentage.GetComponent<Player1Health>();
		MadnessPercentage = GameObject.Find("Player1Madness").transform;
		madnessscript = MadnessPercentage.GetComponent<Player1Madness>();
       
    }

    // Update is called once per frame
    void Update()
    {
		healthscript.LifePercentage = playerHealth;
		madnessscript.MadnessPercentage = playerMadness;
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x) + Mathf.Abs(rb2d.velocity.y));
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

		if (playerHealth <= 0f) {
			transform.GetComponent<SpriteRenderer> ().enabled = false;
			//Destroy (transform.gameObject);
		}

		if (playerMadness >= 100f) {
			transform.GetComponent<SpriteRenderer> ().enabled = false;
			//Destroy (transform.gameObject);
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

    void playSound()
    {
        winchester.Play();
    }
}
