using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	//PlayerCurrency
	public Transform PageCount; //UI element
	Currency pagescript;

	// Use this for initialization
	void Start () {
		PageCount = GameObject.Find ("PageCount").transform; //UI Element
		pagescript = PageCount.GetComponent<Currency> ();
	}
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Currency")
        {
            if (collision.tag == "Player")
            {
				pagescript.count += 1;
                Destroy(gameObject);
            }
        }

        
    }
}
