using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private Animator anim;
    // Use this for initialization

    private void Start()
    {
        anim = GameObject.FindWithTag("Enemy").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            Destroy(collision.gameObject);
        }
    }
}
