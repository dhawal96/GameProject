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
    public float currency;

	//Player1Bullet
    public GameObject bullet;
    public float bulletDamage;
    public Transform spawnPoint;
    public AudioSource[] sounds;
    public bool shotgun;

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

    public WeaponObject[] weapons;
    public int currentWeapon;
    public GameObject gameOverPanel;
    public GameObject gameAudio;
    private bool elixir;
    private bool blink;
    private bool morphine;
    private bool locked;
    public GameObject pausePanel;
    public GameObject enterShopUIPanel;
    public GameObject shopPanel;
    public GameObject gamePlayPanel;
    public float upgradeDamage;


    // Use this for initialization
    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>(); 
        anim = gameObject.GetComponent<Animator>();
        sounds = gameObject.GetComponents<AudioSource>();
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
        shotgun = false;
        currentWeapon = 0;
        currency = 0;
        bulletDamage = 150f;
        elixir = false;
        blink = false;
        morphine = false;
        locked = false;
        upgradeDamage = 0f;
        StartCoroutine(OpenGamePlayPanel());


    }

    IEnumerator OpenGamePlayPanel()
    {
        yield return new WaitForSeconds(2);
        gamePlayPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        markShooting = false;

        if (gamePlayPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.P) && !gameOverPanel.activeSelf && !enterShopUIPanel.activeSelf && !shopPanel.activeSelf && !gamePlayPanel.activeSelf)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;

            /*if (pauseGame)
            {
                Time.timeScale = 1f;
                pauseGame = false;
            }
            else
            {
                Time.timeScale = 0f;
                pauseGame = true;
            }*/
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

            if (Input.GetKeyDown(KeyCode.K) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark"))
            {
                anim.SetBool("Shooting", true);
                shootOnce = true;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (elixir)
                {
                    playerHealth += 50f;
                    playerMadness += 20f;

                    if (playerHealth >= 100f)
                    {
                        playerHealth = 100f;
                    }
                }

                else if (blink)
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
                    if (left)
                    {
                        if (xPosition - 2f < minPos)
                            transform.position = new Vector3(minPos, transform.position.y, transform.position.z);
                        else
                            transform.position = new Vector3(xPosition - 2f, transform.position.y, transform.position.z);
                        //transform.Translate(Vector3.left * speed * Time.deltaTime);
                    }
                    else
                    {
                        if (xPosition + 2f > maxPos)
                            transform.position = new Vector3(maxPos, transform.position.y, transform.position.z);
                        else
                            transform.position = new Vector3(xPosition + 2f, transform.position.y, transform.position.z);
                        //transform.Translate(Vector3.right * speed * Time.deltaTime);
                    }

                }
            }

            if (weapons[currentWeapon].itemCode == "elixir" || weapons[currentWeapon].itemCode == "blink" || weapons[currentWeapon].itemCode == "shotgun" || weapons[currentWeapon].itemCode == "speed" || weapons[currentWeapon].itemCode == "damage")
            {
                locked = false;
            }

            if (locked == false)
            {
                switch (weapons[currentWeapon].itemCode)
                {
                    case "elixir":
                        {
                            elixir = true;
                            blink = false;
                            morphine = false;
                            break;
                        }
                    case "blink": ///work in progress
                        {
                            elixir = false;
                            blink = true;
                            morphine = false;
                            break;
                        }
                    case "morphine":
                        {
                            playerMadness = 0;
                            locked = true;
                            break;
                        }
                    case "shotgun":
                        {
                            shotgun = true;
                            break;
                        }
                    /*case "speed": //These are updated through the WeaponButton script so that it doesn't run in every frame from Update()
                        {
                            if (maxSpeed >= 1.5f)
                            {
                                maxSpeed = 1.5f;
                            }

                            else
                            {
                                maxSpeed += .25f;
                            }
                            break;
                        }
                    case "damage":
                        {
                            bulletDamage = 300f;
                            break;
                        }*/
                    case "null":
                        {
                            //Do Nothing
                            break;
                        }
                    default:
                        break;

                }
            }

                //elixir = false;


            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark") && shootOnce && shotgun == true)
            {
                GameObject bullet1 = (GameObject)Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                bullet1.transform.Rotate(0f, 0f, 10f);
                GameObject bullet2 = (GameObject)Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                bullet2.transform.Rotate(0f, 0f, 0f);
                GameObject bullet3 = (GameObject)Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                bullet3.transform.Rotate(0f, 0f, -10f);
                //Transform newBullet = Instantiate(bullet.transform, spawnPoint.position, Quaternion.identity) as Transform;
                //newBullet.parent = transform;
                markShooting = true;
                shootOnce = false;
            }

            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_Mark") && shootOnce)
			{
                Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                //Transform newBullet = Instantiate(bullet.transform, spawnPoint.position, Quaternion.identity) as Transform;
                //newBullet.parent = transform;
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

        if (dead == true)
        {
            gameAudio.GetComponent<AudioSource>().Stop();
            gameOverPanel.SetActive(true);
        }
    }

    //for physics
    void FixedUpdate()
    {
        Physics2D.gravity = Vector2.zero;
		if (!dead) {
			if (Input.GetKey (KeyCode.A)) {
				rb2d.AddForce (Vector3.left * speed);
                transform.localScale = new Vector3(-2f, 2f, 1f);
                left = true;
            }

			if (Input.GetKey (KeyCode.D)) {
				rb2d.AddForce (Vector3.right * speed);
                transform.localScale = new Vector3(2f, 2f, 1f);
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
		}

    }

    void SetHit()
    {
        gameObject.GetComponent<Animator>().SetBool("Hit", false);
    }

    public void PlayGrunt()
    {
        sounds[1].Play();
    }

    void Destroy()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
    }

    void playSound()
    {
        sounds[0].Play();
    }

}