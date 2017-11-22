using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    //GameObject
    public GameObject player;
    public GameObject healthBar;
    public GameObject fillHealthBar;
    public GameObject gameOverPanel;
    public GameObject rain;
    public GameObject bossSplashEffect;
    public GameObject camera;
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

    //Variables
    public float health;
    public bool exitEntryScene;
    private string[] attacks;
    public bool alreadyHit;
    private bool chooseNewAttack;
    private int attackIndex;

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
                yield return new WaitForSecondsRealtime(5);
                enemySpawnCooldown = false;
                spawningComplete = true;
                chooseNewAttack = true;
            }

            else if (spawner.GetComponent<Spawner>().waveCount != 3f && !enemySpawnCooldown)
            {
                yield return new WaitForSecondsRealtime(5); //Wait time before Dagon's spawning enemies animation begins
                anim.SetBool("SpawnEnemies", true); //End of animation clip will call spawnEnemies() function
                spawningComplete = false;
                chooseNewAttack = true;
            }         
        }

        else if (attacks[attackIndex] == "laser")
        {
            Debug.Log("laser");
            yield return new WaitForSecondsRealtime(5); //Instantiate Lasers and Calculate where Mark is
            leftLaser = Instantiate(laser, new Vector3(97.289f, 4.658f, 0f), laserRotation);
            rightLaser = Instantiate(laser, new Vector3(98.88f, 4.572f, 0f), laserRotation);
            Vector2 direction = new Vector2(player.transform.position.x - leftLaser.transform.position.x, player.transform.position.y - leftLaser.transform.position.y);
            //Debug.Log(direction.x + " and " + direction.y);
            yield return new WaitForSecondsRealtime(1); //Mark has 1 second to react and dodge it (This is where the animation begins)
            leftLaser.transform.up = -direction;
            rightLaser.transform.up = -direction;
            leftLaser.SetActive(true);
            rightLaser.SetActive(true);
            yield return new WaitForSecondsRealtime(5); //How long the laser lasts
            Destroy(leftLaser);
            Destroy(rightLaser);
            chooseNewAttack = true;
            //end the animation here
        }
    }

    // Use this for initialization
    void Start () {

        health = 25000f;
        exitEntryScene = false;
        attacks = new string[] { "spawnEnemies", "laser"};
        anim = GetComponent<Animator>();
        alreadyHit = false;
        spawningComplete = true;
        chooseNewAttack = true;

    }
	
	// Update is called once per frame
	void Update ()
    {
        fillHealthBar.GetComponent<BossHealth>().lifePercentage = health;

        if (health <= 0f)
        {
            Destroy(gameObject);
            gameOverPanel.SetActive(true);
            rain.SetActive(false);
        }

        if (exitEntryScene)
        {
            anim.SetBool("IntroDone", true);
            if (chooseNewAttack)
            {
                if (spawningComplete == false)
                {
                    attackIndex = 0;
                }

                else
                {
                    attackIndex = Random.Range(0, 2);
                }

                Debug.Log(attackIndex);
                StartCoroutine(Attack());
                chooseNewAttack = false;
            }
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
            health -= 150f;
            alreadyHit = true;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb" && alreadyHit == false && collision.gameObject.GetComponent<BulletFire>().exploding == true)
        {
            health -= 150f;
            alreadyHit = true;
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
}
