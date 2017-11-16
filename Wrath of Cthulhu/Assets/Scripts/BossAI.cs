using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    public GameObject player;
    public GameObject healthBar;
    public GameObject fillHealthBar;
    public GameObject gameOverPanel;
    public GameObject rain;
    public float health;


	// Use this for initialization
	void Start () {

        health = 25000f;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        fillHealthBar.GetComponent<BossHealth>().lifePercentage = health;

        if (health <= 0f)
        {
            Destroy(gameObject);
            gameOverPanel.SetActive(true);
            rain.SetActive(false);
        }

    }
}
