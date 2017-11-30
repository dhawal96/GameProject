using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    public float lifePercentage;
    private float maxHealth;

    void Start()
    {
        maxHealth = 36000f;
    }

	
	// Update is called once per frame
	void Update () {

        GetComponent<Image>().fillAmount = lifePercentage / maxHealth;
		
	}
}
