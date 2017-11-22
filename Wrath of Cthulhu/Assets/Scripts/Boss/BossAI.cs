using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    public GameObject player;
    public GameObject healthBar;
    public GameObject fillHealthBar;
    public GameObject gameOverPanel;
    public GameObject rain;
    public GameObject spawner;
    public GameObject bossSplashEffect;
    public GameObject camera;
    public GameObject laser;
    private Animator anim;
    public float health;
    public bool exitEntryScene;
    private string[] attacks;
    public bool alreadyHit;
    private bool waitOnce;
    private bool enemySpawnCooldown;
    private int attackIndex;
    private Quaternion laserRotation;

    IEnumerator WaitTime()
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
                waitOnce = true;
            }

            else if (spawner.GetComponent<Spawner>().waveCount != 3f && !enemySpawnCooldown)
            {
                yield return new WaitForSecondsRealtime(5);
                anim.SetBool("SpawnEnemies", true);
            }         
        }

        else if (attacks[attackIndex] == "laser")
        {
            Debug.Log("laser");
            laserRotation = Quaternion.Euler(0, 0, -74.853f);
            yield return new WaitForSecondsRealtime(5);
            GameObject leftLaser = Instantiate(laser, new Vector3(89.57f, 2.84f, 0f), laserRotation);
            GameObject rightLaser = Instantiate(laser, new Vector3(91.17f, 2.78f, 0f), laserRotation);
            yield return new WaitForSecondsRealtime(5);
            Destroy(leftLaser);
            Destroy(rightLaser);
            waitOnce = true;

        }
    }

    // Use this for initialization
    void Start () {

        health = 25000f;
        exitEntryScene = false;
        attacks = new string[] { "spawnEnemies", "laser"};
        anim = GetComponent<Animator>();
        alreadyHit = false;
        waitOnce = true;

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
            if (waitOnce)
            {
                attackIndex = Random.Range(0, 2);
                Debug.Log(attackIndex);
                StartCoroutine(WaitTime());
                waitOnce = false;
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
