using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionCollider : MonoBehaviour {
    CameraFollow controlscript;
    Player playerScript;
    public Camera Camera;
    GameObject Player;
    private bool breakCollision;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player");
        playerScript = Player.GetComponent<Player>();
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
                    Player.transform.position = new Vector3(gameObject.transform.localPosition.x + .63227f, 3.08f, 0f);
                    controlscript.minCameraPos = new Vector3(22.56f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(28.17f, 5.78f, -10f);
                    playerScript.colliderCount += 1f;
                    playerScript.enemiesRemaining = 5;
                    playerScript.enemiesKilled = 0;
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider2":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 5 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    Player.transform.position = new Vector3(gameObject.transform.localPosition.x + .63227f, 3.08f, 0f);
                    controlscript.minCameraPos = new Vector3(33.35f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(38.38f, 5.80f, -10f);
                    playerScript.colliderCount += 1f;
                    playerScript.enemiesRemaining = 6;
                    playerScript.enemiesKilled = 0;
                    breakCollision = true;
                }
                break;
               
            case "SectionEndCollider3":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 6 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    Player.transform.position = new Vector3(gameObject.transform.localPosition.x + .63227f, 3.08f, 0f);
                    controlscript.minCameraPos = new Vector3(43.59f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(49.01f, 5.80f, -10f);
                    playerScript.colliderCount += 1f;
                    playerScript.enemiesRemaining = 6;
                    playerScript.enemiesKilled = 0;
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider4":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 6 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    Player.transform.position = new Vector3(gameObject.transform.localPosition.x + .63227f, 3.08f, 0f);
                    controlscript.minCameraPos = new Vector3(54.21f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(59.53f, 5.80f, -10f);
                    playerScript.colliderCount += 1f;
                    playerScript.enemiesRemaining = 2;
                    playerScript.enemiesKilled = 0;
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider5":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 2 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    Player.transform.position = new Vector3(gameObject.transform.localPosition.x + .63227f, 3.08f, 0f);
                    controlscript.minCameraPos = new Vector3(64.71f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(73.27f, 5.80f, -10f);
                    playerScript.colliderCount += 1f;
                    playerScript.enemiesRemaining = 7;
                    playerScript.enemiesKilled = 0;
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider6":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 7 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    Player.transform.position = new Vector3(gameObject.transform.localPosition.x + .63227f, 3.08f, 0f);
                    controlscript.minCameraPos = new Vector3(78.46f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(82.24f, 5.80f, -10f);
                    playerScript.colliderCount += 1f;
                    playerScript.enemiesRemaining = 5;
                    playerScript.enemiesKilled = 0;
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider7":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 5 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    Player.transform.position = new Vector3(gameObject.transform.localPosition.x + .63227f, 3.08f, 0f);
                    controlscript.minCameraPos = new Vector3(87.42f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(96.15f, 5.80f, -10f);
                    playerScript.colliderCount += 1f;
                    playerScript.enemiesKilled = 100;
                    breakCollision = true;
                }
                break;

            /*case "SectionEndCollider8":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 50 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    Player.transform.position = new Vector3(gameObject.transform.localPosition.x + .63227f, 3.08f, 0f);
                    controlscript.minCameraPos = new Vector3(101.33f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(109.08f, 5.80f, -10f);
                    playerScript.colliderCount += 1f;
                    breakCollision = true;
                }
                break;

            case "SectionEndCollider9":
                if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>().enemiesKilled >= 55 && breakCollision == false)
                {
                    controlscript.SectionEndCollider = gameObject;
                    gameObject.SetActive(false);
                    Player.transform.position = new Vector3(gameObject.transform.localPosition.x + .63227f, 3.08f, 0f);
                    controlscript.minCameraPos = new Vector3(114.27f, 1.50f, -10f);
                    controlscript.maxCameraPos = new Vector3(125.23f, 5.80f, -10f);
                    playerScript.colliderCount += 1f;
                    breakCollision = true;
                }
                break;*/
        }
    }
}