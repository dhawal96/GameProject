using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {

	private Animator HUDAnim; 
	// Use this for initialization
	void Awake () {
		HUDAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	public void HUDFade () {
		HUDAnim.SetTrigger ("FadeIn");
	}
}
