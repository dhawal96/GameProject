using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackTrigger : MonoBehaviour {

    private GameObject Player;
    Player controlscript;
    private Animator anim;
    private Animator enemyAnim;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        controlscript = Player.GetComponent<Player>();
        anim = Player.GetComponent<Animator>();
        enemyAnim = transform.parent.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && transform.parent.gameObject.GetComponent<EnemyMove>().health > 0f && !anim.GetCurrentAnimatorStateInfo(0).IsName("Damage_Mark") && controlscript.sounds[1].isPlaying == false)
        {
          transform.parent.gameObject.GetComponent<EnemyMove>().Damage();
        }
    }
}
