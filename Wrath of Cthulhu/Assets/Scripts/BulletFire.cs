using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour {

    public float bulletForce = 500f;
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
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "FirePoint")
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

        else if (target.gameObject.tag == "EnemyFirepoint")
        {
            if (gameObject.transform.position.x > Player.transform.position.x)
            {
                GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletForce);
            }

            else
            {
                GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "RangeEnemy")
        {

            collision.gameObject.GetComponent<Animator>().SetBool("Hit", true);
   
            collision.gameObject.GetComponent<EnemyMove>().health -= bulletDamage;
            Destroy(gameObject);

            if (collision.gameObject.GetComponent<EnemyMove>().health <= 0)
            {
                collision.gameObject.GetComponent<Animator>().SetBool("Death", true);

                Destroy(gameObject);
                
            }
        }

        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().playerHealth -= enemyDamage;
            collision.gameObject.GetComponent<Player>().playerMadness += enemyMadness;
            collision.gameObject.GetComponent<Animator>().SetBool("Hit", true);
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

}
