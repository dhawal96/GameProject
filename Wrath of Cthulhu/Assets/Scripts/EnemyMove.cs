using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    private GameObject Player;
    public float speed = 1f;
    public float maxSpeed = .01f;
    public float health;
    public bool idle;
    public float enemyCount = 10f;
    private bool activateEnemy = false;
    public bool enemyShooting;
    private float randomIndex;
    private int itemIndex;
    private Vector3 enposition;
    public DashState dashState;
    public float dashTimer;
    public float maxDash = 20f;
    public Vector2 savedVelocity;
    private float minDistance = .524848f;
    private bool rangeEnemyAttack;
    private bool setDashActive;

    private Animator anim;
    private Rigidbody2D rb2d;
    private float range;
    public float enemyDamage;
    public float enemyMadness;
    RaycastHit2D hit;
    Player controlscript;
    bool contact;

    public GameObject[] items;
    public GameObject mainCamera;
    public Camera camera;
    public Transform spawnPoint;
    public Vector3 playerPosition;
    public GameObject bullet;



    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        controlscript = Player.GetComponent<Player>();
        mainCamera = GameObject.FindWithTag("MainCamera");
        camera = mainCamera.GetComponent<Camera>();
        health = 300f;
        enemyDamage = 25f;
        enemyMadness = 10f;
        contact = false;
        idle = true;
        rangeEnemyAttack = false;
        setDashActive = true;
        dashState = DashState.Ready;
        anim.SetBool("Idle", true);

    }

    void Update()
    {
        enemyShooting = false;

        if (controlscript.dead)
        {
            anim.SetBool("Idle", true);
            idle = true;
            anim.SetFloat("Speed", 0f);
        }
        else {
            range = Vector2.Distance(transform.position, Player.transform.position);
        }

        Vector3 viewPos = camera.WorldToViewportPoint(gameObject.transform.position);

        if (viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1 && viewPos.z > 0 && !controlscript.dead) //activate enemy
        {
            anim.SetBool("Idle", false);
            idle = false;
            anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x) + Mathf.Abs(rb2d.velocity.y));
        }

        if (rangeEnemyAttack == true)
        {
            rb2d.isKinematic = true;
            if (Player.transform.position.x > transform.position.x)
            {
                //face right
                transform.localScale = new Vector3(1.5f, 1.5f, 1);
            }
            else if (Player.transform.position.x < transform.position.x)
            {
                //face left
                transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            }
            anim.SetBool("Attack", true);
        }

        if (gameObject.tag == "Enemy" && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dash_DeepOnes"))
        {
            setDashActive = true;
        }

        if (gameObject.tag == "Enemy")
        {
            
            if (range <= minDistance && !idle && !anim.GetBool("Death") == true && Player.transform.position.y >= gameObject.transform.position.y - .3f && Player.transform.position.y <= gameObject.transform.position.y + .3f)
            {
                rb2d.isKinematic = true;
                anim.SetBool("Attack", true);
                anim.SetBool("Dash", false);

            }
          
            else if (range <= 1.093846f && !idle && !anim.GetBool("Death") == true && Player.transform.position.y >= gameObject.transform.position.y - .3f && Player.transform.position.y <= gameObject.transform.position.y + .3f && setDashActive)
            {
                //dashState = DashState.Ready;
                Debug.Log(dashState);
                switch (dashState)
                {
                    case DashState.Ready:
                        savedVelocity = rb2d.velocity;
                        //rb2d.velocity = new Vector2(rb2d.velocity.x * 3f, rb2d.velocity.y);

                        dashState = DashState.Dashing;
                        break;
                    case DashState.Dashing:
                        if (Player.GetComponent<Player>().dead == true)
                        {
                            dashState = DashState.End;
                        }
                        rb2d.isKinematic = false;
                        anim.SetBool("Dash", true);
                        //rb2d.AddForce((Player.transform.position - transform.position) * 25f);
                        if (transform.localScale.x > 0)
                        {
                            rb2d.AddForce(Vector3.right * 50f);
                        }
                        else
                        {
                            rb2d.AddForce(Vector3.left * 50f);
                        }
                        dashTimer += Time.deltaTime * 3;
                        if (dashTimer >= maxDash)
                        {
                            dashTimer = maxDash;
                            rb2d.velocity = savedVelocity;
                            //dashState = DashState.Cooldown;
                        }

                        break;
                    case DashState.Cooldown:
                        anim.SetBool("Dash", false);
                        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                        dashTimer -= Time.deltaTime;
                        if (dashTimer <= 0)
                        {
                            dashTimer = 0;
                            dashState = DashState.Ready;
                        }
                        break;
                    default:
                        break;
                }
            }

            else if (range > minDistance && !idle && !anim.GetBool("Death") == true && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dash_DeepOnes"))
            {
                rb2d.isKinematic = false;
                anim.SetBool("Attack", false);
                anim.SetBool("Dash", false);

                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                if (Player.transform.position.x > transform.position.x)
                {
                    //face right
                    transform.localScale = new Vector3(1.5f, 1.5f, 1);
                }
                else if (Player.transform.position.x < transform.position.x)
                {
                    //face left
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                }
            }
            
        }

        if (gameObject.tag == "RangeEnemy")
        {
            if (!idle && Player.transform.position.y >= gameObject.transform.position.y - .1f && Player.transform.position.y <= gameObject.transform.position.y + .1f)
            {
                rangeEnemyAttack = true;
            }

            else if (!idle && !anim.GetBool("Death") == true && !rangeEnemyAttack && rb2d.isKinematic == false)
            {
                rb2d.isKinematic = false;
                anim.SetBool("Attack", false);

                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                if (Player.transform.position.x > transform.position.x)
                {
                    //face right
                    transform.localScale = new Vector3(1.5f, 1.5f, 1);
                }
                else if (Player.transform.position.x < transform.position.x)
                {
                    //face left
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                }
            }

        }
    }

    void FixedUpdate()
    {

        Physics2D.gravity = Vector2.zero;

        if (idle == true)
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
        }

        else
        {
            rb2d.velocity = (Player.transform.position - transform.position).normalized * speed;
        }

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.y > maxSpeed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxSpeed);
        }

        if (rb2d.velocity.y < -maxSpeed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -maxSpeed);
        }
    }

    void Damage()
    {

        if (Player.transform.position.y >= gameObject.transform.position.y - .3f && Player.transform.position.y <= gameObject.transform.position.y + .3f)
        {
            Player.GetComponent<Player>().playerHealth -= enemyDamage;
            Player.GetComponent<Player>().playerMadness += enemyMadness;
            Player.GetComponent<Animator>().SetBool("Hit", true);
        }
    }

    void SetHit()
    {
        gameObject.GetComponent<Animator>().SetBool("Hit", false);
    }

    private void Destroy()
    {
        enposition = gameObject.GetComponent<EnemyMove>().transform.position;
        Destroy(gameObject);
        randomIndex = Random.Range(1f, 100f);
        if (randomIndex <= 60f && randomIndex >= 1f)
        {
            //itemIndex = Random.Range(0, collision.gameObject.GetComponent<EnemyMove>().items.Length - 1);
            itemIndex = Random.Range(3, gameObject.GetComponent<EnemyMove>().items.Length);
            GameObject coin = Instantiate(gameObject.GetComponent<EnemyMove>().items[itemIndex], enposition, Quaternion.identity);
            Destroy(coin, 5);
        }

        else
        {
            itemIndex = Random.Range(0, gameObject.GetComponent<EnemyMove>().items.Length - 2);
            GameObject coin = Instantiate(gameObject.GetComponent<EnemyMove>().items[itemIndex], enposition, Quaternion.identity);
            Destroy(coin, 5);
        }
        Player.GetComponent<Player>().enemiesKilled++;
    }

    void InstantiateBullet()
    {
        rangeEnemyAttack = false;
        rb2d.isKinematic = false;
        //Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        Transform newBullet = Instantiate(bullet.transform, spawnPoint.position, Quaternion.identity) as Transform;
        newBullet.GetComponent<BulletFire>().Enemy = gameObject;
        //newBullet.parent = transform;
        enemyShooting = true;
    }

    void getPlayerPosition()
    {
        playerPosition = Player.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "RangeEnemy")
        {
            rb2d.isKinematic = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.tag == "RangeEnemy")
        {
            rb2d.isKinematic = false;
        }
    }

    private void endDashState()
    {
        anim.SetBool("Dash", false);
    }

    private void startDashState()
    {
        setDashActive = false;
    }

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown,
        End
    }

}


