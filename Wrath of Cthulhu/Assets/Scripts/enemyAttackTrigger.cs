using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<EnemyMove>().Damage();
        }
    }
}
