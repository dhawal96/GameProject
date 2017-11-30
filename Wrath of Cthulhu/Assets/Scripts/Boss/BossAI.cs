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
    public GameObject madnessBall;
    private Animator anim;

    //Enemy Spawning
    public GameObject spawner;
    public GameObject spawner2;
    public GameObject spawner3;
    public GameObject spawner4;
    private bool enemySpawnCooldown;
    private bool spawningComplete;
    private bool spawn;

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
    private bool executeOnce;

    // Use this for initialization
    void Start()
    {

        health = 50000f;
        exitEntryScene = false;
        attacks = new string[] { "spawnEnemies", "laser", "madness", "swipe"};
        anim = GetComponent<Animator>();
        alreadyHit = false;
        spawningComplete = true;
        chooseNewAttack = true;
        laserFollowPlayer = false;
        attacking = false;
        swiping = false;
        lockSwipe = false;
        executeOnce = false;
        spawn = false;
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
                yield return new WaitForSeconds(6);
                swiping = true;
                spawn = true;
                anim.SetBool("Swipe", true);
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

                yield return new WaitForSeconds(6); //How long the laser lasts
                laserFollowPlayer = false;
                Destroy(leftLaser);
                Destroy(rightLaser);
                chooseNewAttack = true;
                anim.SetBool("Laser", false);
                laserCollider = false;
            }

            else if (attacks[attackIndex] == "madness")
            {
                yield return new WaitForSeconds(4);
                anim.SetBool("SpawnEnemies", true); //THIS ANIMATiON IS NO LONGER USED TO SPAWN ENEMIES. IT IS USED TO INSTANTIATE MADNESS BALLS
            }

            else if (attacks[attackIndex] == "swipe")
            {
                yield return new WaitForSeconds(4);
                swiping = true;
                anim.SetBool("Swipe", true);
            }  
    }

    IEnumerator MadnessBall()
    {
        GameObject madnessBall1 = (GameObject)Instantiate(madnessBall, spawner.transform.position, spawner.transform.rotation);
        madnessBall1.transform.Rotate(0f, 0f, 10f);
        GameObject madnessBall2 = (GameObject)Instantiate(madnessBall, spawner.transform.position, spawner.transform.rotation);
        madnessBall2.transform.Rotate(0f, 0f, 0f);
        GameObject madnessBall3 = (GameObject)Instantiate(madnessBall, spawner.transform.position, spawner.transform.rotation);
        madnessBall3.transform.Rotate(0f, 0f, -10f);
        GameObject madnessBall4 = (GameObject)Instantiate(madnessBall, spawner.transform.position, spawner.transform.rotation);
        madnessBall4.transform.Rotate(0f, 0f, 20f);
        GameObject madnessBall5 = (GameObject)Instantiate(madnessBall, spawner.transform.position, spawner.transform.rotation);
        madnessBall5.transform.Rotate(0f, 0f, -20f);
        yield return new WaitForSeconds(1);
        GameObject madnessBall6 = (GameObject)Instantiate(madnessBall, spawner.transform.position, spawner.transform.rotation);
        madnessBall6.transform.Rotate(0f, 0f, 5f);
        GameObject madnessBall7 = (GameObject)Instantiate(madnessBall, spawner.transform.position, spawner.transform.rotation);
        madnessBall7.transform.Rotate(0f, 0f, -5f);
        GameObject madnessBall8 = (GameObject)Instantiate(madnessBall, spawner.transform.position, spawner.transform.rotation);
        madnessBall8.transform.Rotate(0f, 0f, 15f);
        GameObject madnessBall9 = (GameObject)Instantiate(madnessBall, spawner.transform.position, spawner.transform.rotation);
        madnessBall9.transform.Rotate(0f, 0f, -15f);
        anim.SetBool("SpawnEnemies", false);
        chooseNewAttack = true;
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

        else if (canLockSwipe)
        {
            lockSwipe = false;
        }

        if (!player.GetComponent<Player>().gameOverPanel.activeSelf) // if the game is not already over
        {
            if (health <= 0f && !executeOnce)
            {
                executeOnce = true;
                anim.SetBool("Dead", true);
                Destroy(leftLaser);
                Destroy(rightLaser);
                GameObject.FindGameObjectWithTag("Music").GetComponent<MainMenuMusic>().stopGameAudio();
                sounds[2].Play();
                player.GetComponent<Player>().bossDead = true;
            }
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

            if (chooseNewAttack && !youWinPanel.activeSelf && player.GetComponent<Player>().dead == false && Time.timeScale == 1)
            {
                chooseNewAttack = false;

                if (lockSwipe)
                {
                    lockSwipe = false;

                    if (amountOfSwipes == 0) //if not already assigned a random value
                    {
                        canLockSwipe = false;
                        amountOfSwipes = Random.Range(1, 4);
                    }

                    attackIndex = 3;
                    amountOfSwipes -= 1;
                }

                else
                {
                    int randomNumber = Random.Range(0, 100);

                    if (randomNumber <= 15)
                    {
                        attackIndex = 0;
                    }

                    else
                    {
                        attackIndex = Random.Range(1, 3);
                    }

                    canLockSwipe = true;
                }

                StartCoroutine(Attack());
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
        if (spawn == true)
        {
            spawn = false;
            activeSwiping = true;
            swipeCollision.SetActive(true);
            sounds[1].Play();
            spawner.SetActive(true);
            spawner2.SetActive(true);
            spawner3.SetActive(true);
            spawner4.SetActive(true);
        }

        else
        {
            activeSwiping = true;
            swipeCollision.SetActive(true);
            sounds[1].Play();
        }
    }

    void EndSwipe()
    {
        spawner.SetActive(false);
        spawner2.SetActive(false);
        spawner3.SetActive(false);
        spawner4.SetActive(false);
        activeSwiping = false;
        swipeCollision.SetActive(false);
        swiping = false;
        anim.SetBool("Swipe", false);
        chooseNewAttack = true;
    }

    void Dead()
    {
        Destroy(gameObject);
        youWinPanel.SetActive(true);
        rain.SetActive(false);
        oldRain.SetActive(true);
    }
}
