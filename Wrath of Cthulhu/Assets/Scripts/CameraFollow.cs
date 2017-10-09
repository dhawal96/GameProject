using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;

    private GameObject player;
    private GameObject SectionEndCollider1;
    private GameObject SectionEndCollider2;
    private bool collider1;
    private bool collider2;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;


    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        SectionEndCollider1 = GameObject.FindGameObjectWithTag("SectionEndCollider1");
        SectionEndCollider2 = GameObject.FindGameObjectWithTag("SectionEndCollider2");
        minCameraPos = new Vector3(2.63f, 1.51f, -10f);
        //maxCameraPos = new Vector3(48.61f, 5.78f, -10f);
        maxCameraPos = new Vector3(17.41f, 5.78f, -10f);
        collider1 = false;
        collider2 = false;

    }

    private void Update()
    {
        /*if (player.GetComponent<Player>().enemiesKilled == 10)
        {
            maxCameraPos = new Vector3(27.43f, 5.78f, -10f);
            SectionEndCollider1.SetActive(false);
        }*/

        if (player.transform.position.x >= 20.21f && player.transform.position.x < 30.19f)
        {
            SectionEndCollider1.SetActive(true);
            minCameraPos = new Vector3(22.58f, 1.51f, -10f);
        }

        if (player.transform.position.x >= 30.19f)
        {
            SectionEndCollider2.SetActive(true);
            minCameraPos = new Vector3(30.19f, 1.51f, -10f);
        }


        /*if (player.transform.position.x >= 22f)
        {
            minCameraPos.x = 22.58f;
        }*/

    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SectionEndCollider1" && collider1 == false)
        {
            if (player.GetComponent<Player>().enemiesKilled == 10)
            {
                collision.gameObject.SetActive(false);
                maxCameraPos = new Vector3(27.43f, 5.78f, -10f);
                collider1 = true;
            }
        }

        if (collision.tag == "SectionEndCollider2" && collider2 == false)
        {
            if (player.GetComponent<Player>().enemiesKilled == 15)
            {
                collision.gameObject.SetActive(false);
                maxCameraPos = new Vector3(48.61f, 5.78f, -10f);
                collider2 = true;
            }
        }
    }
}
