﻿using System.Collections;
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

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").gameObject;
        anim = Player.GetComponent<Animator>();
        enemyDamage = 35f;
        enemyMadness = 15f;
        restrictCurrency = true;
        playerBulletForce = 500f;
        controlscript = Player.GetComponent<Player>();
    }

    private void Update()
    {
        if (GetComponent<Renderer>().isVisible == false)
        {
            Destroy(gameObject);
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
                Destroy(gameObject);
                break;
        }
    }


}
