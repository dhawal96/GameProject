using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoss : MonoBehaviour {

    public  Camera mainCamera;
    private CameraFollow controlscript;


    private void Start()
    {
        controlscript = mainCamera.GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<MainMenuMusic>().stopGameAudio();
            controlscript.stayOnPlayer = false;
            controlscript.minCameraPos = new Vector3(91.3f, 3.65f, -10f);
            controlscript.maxCameraPos = new Vector3(95.75f, 3.65f, -10f);
            collision.gameObject.GetComponent<Player>().canMove = false;
        }
    }
}
