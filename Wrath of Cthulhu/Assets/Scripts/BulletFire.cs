using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

    private float playerBulletForce;
    private GameObject Player;
    Player controlscript;
    private Vector3 enposition;
    private int itemIndex;
    private float randomIndex;
    private bool restrictCurrency;
    public float enemyDamage;
    public float enemyMadness;
    public GameObject Enemy;
    private Animator anim;

    //Bomb Variables
    private bool forceAdded;
    private bool bombLanded;
    private bool lockCollision;
    private bool enemyAlreadyHit;
    public bool exploding;


    IEnumerator BombWaitTime()
    {
        yield return new WaitForSecondsRealtime(.5f);
        forceAdded = true;
    }

    IEnumerator ExplosionWaitTime()
    {
        yield return new WaitForSecondsRealtime(.50f);
        gameObject.GetComponent<Animator>().SetBool("Explode", true);
        exploding = true;
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
        anim = Player.GetComponent<Animator>();
        enemyDamage = 35f;
        enemyMadness = 15f;
        restrictCurrency = true;
        lockCollision = false;
        enemyAlreadyHit = false;
        playerBulletForce = 500f;
        controlscript = Player.GetComponent<Player>();
    }

    private void Update()
    {
    
        /*if (gameObject.tag == "Bomb" && forceAdded)
        {
            Debug.Log("Here is my current position" + transform.localPosition.y);
            if (gameObject.transform.localPosition.y <= Player.transform.localPosition.y)
            {
                Debug.Log("In position" + transform.localPosition.y);
                Destroy(gameObject.GetComponent<Rigidbody2D>());
            }
        }*/

        if (forceAdded && gameObject.transform.localPosition.y <= Player.transform.localPosition.y)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.transform.Rotate(0, 0, 0);
        }

    }


    void OnTriggerEnter2D(Collider2D target)
    {
        switch (target.gameObject.tag)
        {
            case "Bullet":
                //Do Nothing
                break;
            case "FirePoint":

                if (gameObject.tag == "Bullet")
                {
                    if (Player.transform.localScale.x > 0)
                    {
                        GetComponent<Rigidbody2D>().AddForce(transform.right * playerBulletForce);

                    }

                    else
                    {
                        GetComponent<Rigidbody2D>().AddForce(-transform.right * playerBulletForce);
                    }

                    controlscript.lockTransform = false;

                }

                else if (gameObject.tag == "Bomb")
                {
                    if (Player.transform.localScale.x > 0)
                    {
                        transform.Rotate(0, 0, 60);
                        GetComponent<Rigidbody2D>().AddForce(transform.right * 200f);

                    }

                    else
                    {
                        transform.Rotate(0, 0, -60);
                        GetComponent<Rigidbody2D>().AddForce(-transform.right * 200f);
                    }

                    controlscript.lockTransform = false;
                    StartCoroutine(BombWaitTime());
                }
                break;

            case "EnemyFirepoint":
                if (gameObject.tag == "EnemyBullet")
                {
                    if (Enemy.transform.localScale.x < 0)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector3(-2.5f, 0f, 0f);
                    }

                    else
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector3(2.5f, 0f, 0f);
                    }

                }
                break;

            case "Enemy":
                if (gameObject.tag == "Bullet")
                {
                    target.gameObject.GetComponent<Animator>().SetBool("Hit", true);

                    target.gameObject.GetComponent<EnemyMove>().health -= controlscript.bulletDamage;
                    Destroy(gameObject);

                    if (target.gameObject.GetComponent<EnemyMove>().health <= 0)
                    {
                        target.gameObject.GetComponent<Animator>().SetBool("Death", true);

                        Destroy(gameObject);
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
                break;

            case "RangeEnemy":
                if (gameObject.tag == "Bullet")
                {
                    target.gameObject.GetComponent<Animator>().SetBool("Hit", true);

                    target.gameObject.GetComponent<EnemyMove>().health -= controlscript.bulletDamage;
                    Destroy(gameObject);

                    if (target.gameObject.GetComponent<EnemyMove>().health <= 0)
                    {
                        target.gameObject.GetComponent<Animator>().SetBool("Death", true);

                        Destroy(gameObject);
                    }
                }
                break;

            case "Player":
                if (anim.GetBool("Blink") == false)
                {
                    target.gameObject.GetComponent<Player>().playerHealth -= enemyDamage;
                    target.gameObject.GetComponent<Player>().playerMadness += enemyMadness;
                    controlscript.sounds[1].Play();
                    target.gameObject.GetComponent<Animator>().SetBool("Hit", true);
                    anim.SetBool("Blink", true);
                    Destroy(gameObject);
                }
                break;

            case "Currency":
                break;

            case "BrokenBridge":
                break;

            case "EnemyBullet":
                if (gameObject.tag == "Bomb")
                {
                    break; 
                }
                break;

            case "Boss":
                if (gameObject.tag == "Bullet")
                {
                    target.gameObject.GetComponent<BossAI>().health -= controlscript.bulletDamage;
                    Destroy(gameObject);
                }

                else
                {
                    Destroy(gameObject);
                }

                break;

            default:
                if (gameObject.tag != "Bomb")
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (gameObject.tag == "Bomb" && collision.gameObject.tag == "Player" && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Explosion") && lockCollision == false)
        {
            lockCollision = true;
            Player.GetComponent<Player>().playerHealth -= 20f;
            Player.GetComponent<Player>().playerMadness += enemyMadness;
            controlscript.sounds[1].Play();
            Player.GetComponent<Animator>().SetBool("Hit", true);
        }*/

        if (gameObject.tag == "Bomb" && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "RangeEnemy" || collision.gameObject.tag == "Boss"))
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.transform.Rotate(0, 0, 0);
        }

        if (gameObject.tag == "Bomb" && collision.gameObject.GetComponent<EnemyMove>().enemyAlreadyHit == false && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "RangeEnemy") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Explosion"))
        {
            collision.gameObject.GetComponent<EnemyMove>().enemyAlreadyHit = true;
            collision.gameObject.GetComponent<Animator>().SetBool("Hit", true);
            collision.gameObject.GetComponent<EnemyMove>().health -= 150f;

                if (collision.gameObject.GetComponent<EnemyMove>().health <= 0)
                {
                    collision.gameObject.GetComponent<Animator>().SetBool("Death", true);
                }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        /*if (gameObject.tag == "Bomb" && collision.gameObject.tag == "Player" && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Explosion") && lockCollision == false)
        {
            lockCollision = true;
            Player.GetComponent<Player>().playerHealth -= 20f;
            Player.GetComponent<Player>().playerMadness += enemyMadness;
            controlscript.sounds[1].Play();
            Player.GetComponent<Animator>().SetBool("Hit", true);
        }*/

        if (gameObject.tag == "Bomb" && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "RangeEnemy" || collision.gameObject.tag == "Boss"))
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.transform.Rotate(0, 0, 0);
        }

        if (gameObject.tag == "Bomb" && collision.gameObject.GetComponent<EnemyMove>().enemyAlreadyHit == false && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "RangeEnemy") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Explosion"))
        {

            collision.gameObject.GetComponent<EnemyMove>().enemyAlreadyHit = true;
            collision.gameObject.GetComponent<Animator>().SetBool("Hit", true);
            collision.gameObject.GetComponent<EnemyMove>().health -= 150f;

                if (collision.gameObject.GetComponent<EnemyMove>().health <= 0)
                {
                    collision.gameObject.GetComponent<Animator>().SetBool("Death", true);
                }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "RangeEnemy") && collision.gameObject.GetComponent<EnemyMove>().enemyAlreadyHit == true)
        {
            collision.gameObject.GetComponent<EnemyMove>().enemyAlreadyHit = false;
        }
    }

    void ExplodeBomb()
    {
        StartCoroutine(ExplosionWaitTime());
    }

    void DestroyBombGameObject()
    {
        Destroy(gameObject);
    }
}
