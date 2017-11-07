using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour {

    public float countAmmo;

    private void Start()
    {
        countAmmo = 12f;        
    }
    // Update is called once per frame
    void Update()
    {
        if (countAmmo <= 9f)
        {
            GetComponent<Text>().text = "   " + countAmmo + " / 12";
        }
        else
        {
            GetComponent<Text>().text = countAmmo + " / 12";
        }
    }
}
