using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDescription : MonoBehaviour {

	public int cost = 0;
	public string description = "";

	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = "Cost: "+cost+"\n "+description;
	}
}
