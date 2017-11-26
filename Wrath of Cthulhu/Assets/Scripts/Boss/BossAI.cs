using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    //GameObject
    public GameObject player;
    public GameObject healthBar;
    public GameObject fillHealthBar;
    public GameObject youWinPanel;
    public GameObject rain;
    public GameObject oldRain;
    public GameObject bossSplashEffect;
    public GameObject camera;
    public GameObject swipeCollision;
    private Animator anim;

    //Enemy Spawning
    public GameObject spawner;
    private bool enemySpawnCooldown;
    private bool spawningComplete;

    //Laser
    private Quaternion laserRotation;
    public GameObject laser;
    private GameObject leftLaser;
    private GameObject rightLaser;
    private bool laserFollowPlayer;
    private Vector2 direction;

    //Swiping
    private bool swiping; //animation
    private bool activeSwiping; //exact stage where swipe occurs
    private bool lockSwipe;
    private bool canLockSwipe;
    private int maxSwipes;
    private int amountOfSwipes; //Will be Random Int (max 3)

    //Variables
    public float health;
    public bool exitEntryScene;
    private string[] attacks;
    public bool alreadyHit;
    private bool chooseNewAttack;
    private int attackIndex;
    private PolygonCollider2D[] colliders;
    private bool attacking;
    private bool laserCollider;
    private AudioSource[] sounds;


    // Use this for initialization
    void Start()
    {

        health = 50000f;
        exitEntryScene = false;
        attacks = new string[] { "spawnEnemies", "laser", "swipe" };
        anim = GetComponent<Animator>();
        alreadyHit = false;
        spawningComplete = true;
        chooseNewAttack = true;
        laserFollowPlayer = false;
        attacking = false;
        swiping = false;
        lockSwipe = false;
        amountOfSwipes = 0;
        canLockSwipe = true;
        maxSwipes = 0;
        colliders = GetComponents<PolygonCollider2D>();
        sounds = GetComponents<AudioSource>();

    }

    IEnumerator Attack()
    {
            if (attacks[attackIndex] == "spawnEnemies")
            {
                if (spawner.GetComponent<Spawner>().waveCount == 3f)
                {
                    enemySpawnCooldown = true;
                    spawner.GetComponent<Spawner>().waveCount = 0f;
                    spawner.SetActive(false);
                    anim.SetBool("SpawnEnemies", false);
                    attacking = false;
                    yield return new WaitForSeconds(4);
                    enemySpawnCooldown = false;
                    spawningComplete = true;
                    chooseNewAttack = true;
                }

                else if (spawner.GetComponent<Spawner>().waveCount != 3f && !enemySpawnCooldown)
                {
                    yield return new WaitForSeconds(4); //Wait time before Dagon's spawning enemies animation begins
                    anim.SetBool("SpawnEnemies", true); //End of animation clip will call spawnEnemies() function
                    attacking = true;
                    spawningComplete = false;
                    chooseNewAttack = true;
                }
            }

            else if (attacks[attackIndex] == "laser")
            {
                yield return new WaitForSeconds(4); //Instantiate Lasers and Calculate where Mark is
                leftLaser = Instantiate(laser, new Vector3(96.829f, 5.205f, 0f), laserRotation);
                rightLaser = Instantiate(laser, new Vector3(98.446f, 5.588f, 0f), laserRotation);
                direction = new Vector2(player.transform.position.x - leftLaser.transform.position.x, player.transform.position.y - leftLaser.transform.position.y);
                //Debug.Log(direction.x + " and " + direction.y);

                yield return new WaitForSeconds(1); //Mark has 1 second to react and dodge it (This is where the animation begins)
                anim.SetBool("Laser", true);
                laserCollider = true;

                yield return new WaitForSeconds(5); //How long the laser lasts
                laserFollowPlayer = false;
                Destroy(leftLaser);
                Destroy(rightLaser);
                chooseNewAttack = true;
                anim.SetBool("Laser", false);
                laserCollider = false;
            }

            else if (attacks[attackIndex] == "swipe")
            {
                yield return new WaitForSeconds(4);
                swiping = true;
                anim.SetBool("Swipe", true);
            }  
    }

	
	// Update is called once per frame
	void Update ()
    {
        fillHealthBar.GetComponent<BossHealth>().lifePercentage = health;

        if (!swiping)
        {
            transform.position = new Vector3(98.39f, 5.31f, 0f); //Ensures that Dagon never moves out of place, but continues to keep a dynamic rigidbody
        }
        
        else if (swiping)
        {
            transform.position = new Vector3(97.25f, 5.4f, 0f); //Ensures that Dagon never moves out of place, but continues to keep a dynamic rigidbody
        }

        if (player.transform.position.x >= 92.83071f && canLockSwipe)
        {
            lockSwipe = true;
        }

        if (health <= 0f)
        {
            Destroy(gameObject);
            player.GetComponent<Player>().bossDead = true;
            youWinPanel.SetActive(true);
            rain.SetActive(false);
            oldRain.SetActive(true);
            GameObject.FindGameObjectWithTag("Music").GetComponent<MainMenuMusic>().stopGameAudio();
        }

        if (!attacking && laserCollider == false && swiping == false && !activeSwiping)
        {
            colliders[0].enabled = true; //Idle
            colliders[1].enabled = false;
            colliders[2].enabled = false;
            colliders[3].enabled = false;
            colliders[4].enabled = false;
        }

        else if (attacking && laserCollider == false && swiping == false && !activeSwiping)
        {
            colliders[0].enabled = false;
            colliders[1].enabled = true; //Spawning Enemies
            colliders[2].enabled = false;
            colliders[3].enabled = false;
            colliders[4].enabled = false;

        }

        else if (laserCollider == true && swiping == false && !activeSwiping)
        {
            colliders[0].enabled = false;
            colliders[1].enabled = false;
            colliders[2].enabled = true; //Laser
            colliders[3].enabled = false;
            colliders[4].enabled = false;
        }

        else if (swiping == true && !activeSwiping)
        {
            colliders[0].enabled = false;
            colliders[1].enabled = false;
            colliders[2].enabled = false; 
            colliders[3].enabled = true; //Swiping
            colliders[4].enabled = false;
        }

        else if (activeSwiping)
        {
            colliders[0].enabled = false;
            colliders[1].enabled = false;
            colliders[2].enabled = false;
            colliders[3].enabled = false; 
            colliders[4].enabled = true; //Active Swiping
        }

        if (exitEntryScene)
        {
            anim.SetBool("IntroDone", true);

            if (chooseNewAttack && !youWinPanel.activeSelf && Time.timeScale == 1)
            {
                if (spawningComplete == false)
                {
                    attackIndex = 0;
                }

                else if (lockSwipe)
                {
                    lockSwipe = false;

                    if (amountOfSwipes == 0) //if not already assigned a random value
                    {
                        canLockSwipe = false;
                        amountOfSwipes = Random.Range(1, 4);
                    }

                    attackIndex = 2;
                    amountOfSwipes -= 1;
                }

                else
                {
                    attackIndex = Random.Range(0, 2);
                    canLockSwipe = true;
                }

                StartCoroutine(Attack());
                chooseNewAttack = false;
            }
        }
        
        if (laserFollowPlayer && Time.timeScale == 1)
        {
            direction = new Vector2(player.transform.position.x - leftLaser.transform.position.x, player.transform.position.y - leftLaser.transform.position.y);
            leftLaser.transform.up = Vector3.Lerp(leftLaser.transform.up, -direction, 0.002f);
            rightLaser.transform.up = Vector3.Lerp(rightLaser.transform.up, -direction, 0.002f);
        }
    }

    public void EndSplashAndCameraShake()
    {
        camera.GetComponent<CameraControl>()._isShaking = false;
        camera.GetComponent<CameraControl>()._shakeCount = 0;
        Destroy(bossSplashEffect);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb" && alreadyHit == false && collision.gameObject.GetComponent<BulletFire>().exploding == true)
        {
            alreadyHit = true;
            health -= 150f;
            
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb" && alreadyHit == false && collision.gameObject.GetComponent<BulletFire>().exploding == true)
        {
            alreadyHit = true;
            health -= 150f;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb" && alreadyHit)
        {
            alreadyHit = false;
        }
    }

    void spawnEnemies()
    {
        spawner.SetActive(true);
    }

    void createLaser()
    {
        leftLaser.transform.up = -direction;
        rightLaser.transform.up = -direction;
        leftLaser.SetActive(true);
        rightLaser.SetActive(true);
        laserFollowPlayer = true;
    }

    void Swiping()
    {
        activeSwiping = true;
        swipeCollision.SetActive(true);
        sounds[1].Play();
    }

    void EndSwipe()
    {
        activeSwiping = false;
        swipeCollision.SetActive(false);
        swiping = false;
        anim.SetBool("Swipe", false);
        chooseNewAttack = true;
    }
}
