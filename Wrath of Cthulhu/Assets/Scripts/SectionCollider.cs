using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionCollider : MonoBehaviour {
    CameraFollow controlscript;
    GameObject Camera;
    GameObject Player;
    private bool collider1;
    private bool collider2;
    private bool newMin1;
    private bool newMin2;

	// Use this for initialization
	void Start () {
        Camera = GameObject.FindWithTag("MainCamera");
        Player = GameObject.FindWithTag("Player");
        controlscript = Camera.GetComponent<CameraFollow>();
        collider1 = false;
        collider2 = false;
        newMin1 = false;
        newMin2 = false;


    }
	
	// Update is called once per frame
	void Update ()
    {
        /*if (Player.transform.position.x >= 20.21f && newMin1 == false)
        {
            gameObject.SetActive(true);
            controlscript.minCameraPos = new Vector3(22.58f, 1.51f, -10f);
            newMin1 = true;
        }*/

        /*if (Player.transform.position.x >= 30.4 && newMin2 == false)
        {
            gameObject.SetActive(true);
            controlscript.minCameraPos = new Vector3(32.62f, 1.51f, -10f);
            newMin2 = true;
        }*/	
	}

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player" && gameObject.tag == "SectionEndCollider1" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 10 && collider1 == false)
        {
           gameObject.SetActive(false);
           //gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            controlscript.minCameraPos = new Vector3(22.56f, 1.51f, -10f);
            controlscript.maxCameraPos = new Vector3(27.43f, 5.78f, -10f);
           collider1 = true;
        }
    }
}
