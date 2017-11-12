using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoss : MonoBehaviour {

    public  GameObject mainCamera;
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
        }
    }
}
