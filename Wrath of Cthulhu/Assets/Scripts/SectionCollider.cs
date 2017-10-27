using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionCollider : MonoBehaviour {
    CameraFollow controlscript;
    GameObject Camera;
    GameObject Player;
    private bool breakCollision;

	// Use this for initialization
	void Start () {
        Camera = GameObject.FindWithTag("MainCamera");
        Player = GameObject.FindWithTag("Player");
        controlscript = Camera.GetComponent<CameraFollow>();
        breakCollision = false;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        switch (gameObject.name)
        {
            case "SectionEndCollider":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 10 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(22.56f, 1.51f, -10f);
                    controlscript.maxCameraPos = new Vector3(28.17f, 5.78f, -10f);
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider2":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 15 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    controlscript.minCameraPos = new Vector3(33.35f, 1.51f, -10f);
                    controlscript.maxCameraPos = new Vector3(125.23f, 5.78f, -10f);
                    breakCollision = true;
                }
                break;
        }
    }
}