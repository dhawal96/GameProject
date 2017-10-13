using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

    public float bulletForce;
    private GameObject Player;
    private float bulletDamage;
    private Vector3 enposition;
    private int itemIndex;
    private float randomIndex;
    private bool restrictCurrency;
    public float enemyDamage;
    public float enemyMadness;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
        bulletDamage = 100f;
        enemyDamage = 25f;
        enemyMadness = 10f;
        restrictCurrency = true;

        if (gameObject.tag == "Bullet")
        {
            bulletForce = 500f;
        }

        else
        {
            bulletForce = 100f;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        switch (target.gameObject.tag)
        {
            case "FirePoint":
                if (gameObject.tag == "Bullet")
                {
                    if (Player.transform.localScale.x > 0)
                    {
                        GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce);

                    }

                    else
                    {
                        GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletForce);
                    }

                }
                break;

            case "EnemyFirepoint":
                if (gameObject.tag == "EnemyBullet")
                {
                    if (target.gameObject.transform.position.x > Player.transform.position.x)
                    {
                        GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletForce);
                    }

                    else
                    {
                        GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce);
                    }

                }
                break;

            case "Enemy":
                if (gameObject.tag == "Bullet")
                {
                    target.gameObject.GetComponent<Animator>().SetBool("Hit", true);

                    target.gameObject.GetComponent<EnemyMove>().health -= bulletDamage;
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

                    target.gameObject.GetComponent<EnemyMove>().health -= bulletDamage;
                    Destroy(gameObject);

                    if (target.gameObject.GetComponent<EnemyMove>().health <= 0)
                    {
                        target.gameObject.GetComponent<Animator>().SetBool("Death", true);

                        Destroy(gameObject);
                    }
                }
                break;

            case "Player":
                target.gameObject.GetComponent<Player>().playerHealth -= enemyDamage;
                target.gameObject.GetComponent<Player>().playerMadness += enemyMadness;
                target.gameObject.GetComponent<Animator>().SetBool("Hit", true);
                Destroy(gameObject);
                break;

            default:
                Destroy(gameObject);
                break;
        }
    }
}
