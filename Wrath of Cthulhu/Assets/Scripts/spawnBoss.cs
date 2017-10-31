using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoss : MonoBehaviour {

    public GameObject boss;
    public GameObject player;
    public GameObject rain;
    private Player controlPlayerScript;

    private void Start()
    {
        boss.SetActive(false);
        controlPlayerScript = player.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boss.SetActive(true);
            controlPlayerScript.gameAudio.GetComponent<AudioSource>().Stop();
        }
    }
}
