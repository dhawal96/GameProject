using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour {

	public int count;

	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = "x "+count;
	}
}
