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

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
        bulletDamage = 100f;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
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

        else
        {
            Destroy(gameObject);
        }
    }

}
