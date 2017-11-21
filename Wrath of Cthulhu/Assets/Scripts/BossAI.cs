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
    private Animator anim;
    public float health;
    public bool exitEntryScene;
    private string[] attacks;
    public bool alreadyHit;

    IEnumerator WaitTime()
    {
        if (attacks[0] == "spawnEnemies" && spawner.GetComponent<Spawner>().waveCount != 3f)
        {
            yield return new WaitForSecondsRealtime(5);
            spawner.SetActive(true);
        }

        if (spawner.GetComponent<Spawner>().waveCount == 3f)
        {
            spawner.SetActive(false);
            spawner.GetComponent<Spawner>().waveCount = 0f;
        }
        
    }

    // Use this for initialization
    void Start () {

        health = 25000f;
        exitEntryScene = false;
        attacks = new string[] { "spawnEnemies"};
        anim = GetComponent<Animator>();
        alreadyHit = false;

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
        }

        StartCoroutine(WaitTime());
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
}
