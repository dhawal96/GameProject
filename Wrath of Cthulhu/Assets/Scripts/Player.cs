using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


	//Player1Health
    public float playerHealth;
	public Transform HealthPercentage;
	Player1Health healthscript;

	//Player1Madness
	public float playerMadness;
	public Transform MadnessPercentage;
	Player1Madness madnessscript;

	//Camera
	public float minCameraPosX;
	public float maxCameraPosX;
	public Transform CameraFollow;
	CameraFollow camerascript;

    //Player1Currency
    public float currency = 0;

	//Player1Bullet
    public GameObject bullet;
    public Transform spawnPoint;
    private AudioSource winchester;

	//Anim
    private Animator anim;
    private Vector3 input;
    private Rigidbody2D rb2d;
    private bool shootOnce;
	private bool left; 

	//Player variables
    public bool pauseGame;
    public string item;
    public float maxSpeed;
    public float speed;
    public float enemiesKilled = 0f;
	public bool dead;
    public bool markShooting;
    public float minPos;
    public float maxPos;


    // Use this for initialization
    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        winchester = gameObject.GetComponent<AudioSource>();
        playerHealth = 100f;
		playerMadness = 0f;
		item = "null";
        pauseGame = false;
		HealthPercentage = GameObject.Find("Player1Health").transform;
		healthscript = HealthPercentage.GetComponent<Player1Health>();
		MadnessPercentage = GameObject.Find("Player1Madness").transform;
		madnessscript = MadnessPercentage.GetComponent<Player1Madness>();
		CameraFollow = GameObject.Find("Main Camera").transform;
		camerascript = CameraFollow.GetComponent<CameraFollow>();
        maxSpeed = .5f;
        speed = 50;
		dead = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        markShooting = false;
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseGame)
            {
                Time.timeScale = 1f;
                pauseGame = false;
            }
            else
            {
                Time.timeScale = 0f;
                pauseGame = true;
            }
        }

        if (Input.GetKey(KeyCode.H))
        {
            playerHealth = 100f;
            playerMadness = 0f;
        }

        else if (dead == false)
        {
			anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x) + Mathf.Abs(rb2d.velocity.y));
			anim.SetBool("Shooting", false);

			if (Input.GetKeyDown("f") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark"))
			{
				anim.SetBool("Shooting", true);
				shootOnce = true;
			}

			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark") && shootOnce)
			{
                //Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                Transform newBullet = Instantiate(bullet.transform, spawnPoint.position, Quaternion.identity) as Transform;
                newBullet.parent = transform;
                markShooting = true;
				shootOnce = false;
			}

			if (playerHealth <= 0f) {
                anim.SetBool("Dead", true);
				dead = true;          
				//Destroy (transform.gameObject);
			}

			if (playerMadness >= 100f) {
                anim.SetBool("Dead", true);
				dead = true;
                //Destroy (transform.gameObject);
            }

			healthscript.LifePercentage = playerHealth;
			madnessscript.MadnessPercentage = playerMadness;

		}

    }

    //for physics
    void FixedUpdate()
    {
        Physics2D.gravity = Vector2.zero;
		if (!dead) {
			if (Input.GetKey (KeyCode.A)) {
				rb2d.AddForce (Vector3.left * speed);
				transform.localScale = new Vector3 (-2f, 2f, 1f);
				left = true;
			}

			if (Input.GetKey (KeyCode.D)) {
				rb2d.AddForce (Vector3.right * speed);
				transform.localScale = new Vector3 (2f, 2f, 1f);
				left = false;
			}

			if (Input.GetKey (KeyCode.W)) {
				rb2d.AddForce (Vector3.up * speed);
			}

			if (Input.GetKey (KeyCode.S)) {
				rb2d.AddForce (Vector3.down * speed);
			}
		

			if (rb2d.velocity.x > maxSpeed) {
				rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
			}

			if (rb2d.velocity.x < -maxSpeed) {
				rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
			}

			if (rb2d.velocity.y > maxSpeed) {
				rb2d.velocity = new Vector2 (rb2d.velocity.x, maxSpeed);
			}

			if (rb2d.velocity.y < -maxSpeed) {
				rb2d.velocity = new Vector2 (rb2d.velocity.x, -maxSpeed);
			}

			if (Input.GetKeyDown ("g")) {
				switch (item) {
				case "elixir":
					{
						playerHealth += 50f;
						playerMadness += 20f;

						if (playerHealth >= 100f) {
							playerHealth = 100f;
						}
						break;
					}
				case "blink": ///work in progress
					{
                            playerMadness += 1f;
                            if (transform.position.x >= .25f && transform.position.x <= 19.74f)
                            {
                                minPos = .25f;
                                maxPos = 19.74f;
                            }

                            else if (transform.position.x >= 20.23 && transform.position.x <= 29.81f)
                            {
                                minPos = 20.23f;
                                maxPos = 29.81f;
                            }

                            else if (transform.position.x >= 30.27 && transform.position.x <= 42.04f)
                            {
                                minPos = 30.27f;
                                maxPos = 42.04f;
                            }
                            //float cameraSize = 2.294f;
                            float xPosition = transform.position.x;
						if (left) {
							if (xPosition - 2f < minPos)
								transform.position = new Vector3 (minPos, transform.position.y, transform.position.z);
							else
								transform.position = new Vector3 (xPosition - 2f, transform.position.y, transform.position.z);
							//transform.Translate(Vector3.left * speed * Time.deltaTime);
						} else {
							if (xPosition + 2f > maxPos)
								transform.position = new Vector3 (maxPos, transform.position.y, transform.position.z);
							else
								transform.position = new Vector3 (xPosition + 2f, transform.position.y, transform.position.z);
							//transform.Translate(Vector3.right * speed * Time.deltaTime);
						}
						break;
					}
                  case "null":
					{
						//Do Nothing
						break;
					}
				default:
					break;

				}

				//elixir = false;
			}
		}

    }

    void SetHit()
    {
        gameObject.GetComponent<Animator>().SetBool("Hit", false);
    }

    void Destroy()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
    }

    void playSound()
    {
        winchester.Play();
    }
}