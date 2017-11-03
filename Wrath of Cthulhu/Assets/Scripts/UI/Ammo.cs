using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour {

    public float countAmmo;

    private void Start()
    {
        countAmmo = 6f;        
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = countAmmo + " / A m m o";
    }
}
