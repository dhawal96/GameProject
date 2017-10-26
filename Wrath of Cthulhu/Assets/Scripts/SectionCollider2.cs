using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionCollider2 : MonoBehaviour {

    CameraFollow controlscript;
    GameObject Camera;
    GameObject Player;
    private bool collider1;
    private bool collider2;
    private bool newMin1;
    private bool newMin2;

    // Use this for initialization
    void Start()
    {
        Camera = GameObject.FindWithTag("MainCamera");
        Player = GameObject.FindWithTag("Player");
        controlscript = Camera.GetComponent<CameraFollow>();
        collider1 = false;
        collider2 = false;
        newMin1 = false;
        newMin2 = false;


    }

    // Update is called once per frame
    void Update()
    {


        if (Player.transform.position.x >= 30.4)
        {
            gameObject.SetActive(true);
            //controlscript.minCameraPos = new Vector3(32.62f, 1.51f, -10f);
            newMin2 = true;
        }


    }

    private void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject.tag == "Player" && gameObject.tag == "SectionEndCollider2" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 15 && collider2 == false)
        {
            gameObject.SetActive(false);
            controlscript.minCameraPos = new Vector3(33.35f, 1.51f, -10f);
            controlscript.maxCameraPos = new Vector3(125.23f, 5.78f, -10f);
            collider2 = true;
        }
    }
}

