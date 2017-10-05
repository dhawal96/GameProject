using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Currency")
        {
            if (collision.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().currency += 1;
                Destroy(gameObject);
            }
        }

        if (gameObject.tag == "Elixir")
        {
            if (collision.tag == "Player")
            {
                if (collision.gameObject.GetComponent<Player>().item != "elixir")
                {
                    collision.gameObject.GetComponent<Player>().item = "elixir";

                }
                //collision.gameObject.GetComponent<Player>().playerHealth += 50f;
                Destroy(gameObject);
            }
        }
    }
}
