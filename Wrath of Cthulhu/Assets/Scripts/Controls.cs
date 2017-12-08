using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public bool left;
    public bool right;
    public bool up;
    public bool down;

    public PolygonCollider2D[] colliders;

	// Use this for initialization
	void Start () {

        left = false;
        right = false;
        up = false;
        down = false;
        colliders = gameObject.GetComponents<PolygonCollider2D>();
        Physics2D.queriesHitTriggers = true;

    }

    public void MoveUp()
    {
        up = true;
    }

    public void StopUp()
    {
        up = false;
    }

    public void MoveLeft()
    {
        left = true;
    }

    public void StopLeft()
    {
        left = false;
    }

    public void MoveRight()
    {
        right = true;
    }

    public void StopRight()
    {
        right = false;
    }

    public void MoveDown()
    {
        down = true;
    }

    public void StopDown()
    {
        down = false;
    }
}
