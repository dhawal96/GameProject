using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {

        velocity = new Vector2(Random.Range(MinVelocity.x, MaxVelocity.x), Random.Range(MinVelocity.y, MaxVelocity.y));

        ActualLifeSpan = LifeSpan * Random.Range(0.9f, 1.1f);

        TimeAlive = 0;

		
	}

    public Vector2 MinVelocity = new Vector2(-0.01f, 0.025f);
    public Vector2 MaxVelocity = new Vector2(0.01f, 0.05f);
    float ActualLifeSpan;
    public float LifeSpan = 2f;
    float TimeAlive;

    Vector2 velocity;
	
	// Update is called once per frame
	void Update () {

        TimeAlive += Time.deltaTime;

        if (TimeAlive  >= ActualLifeSpan)
        {
            SimplePool.Despawn(gameObject);
            return;
        }
        this.transform.Translate(velocity * Time.deltaTime);
		
	}
}
