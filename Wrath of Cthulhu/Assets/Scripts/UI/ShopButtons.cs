using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtons : MonoBehaviour {
	private Animator shopAnim;
	// Use this for initialization
	void Awake () {
		shopAnim = GetComponent<Animator> ();
	}
	public void MenuFade(){
		shopAnim.SetTrigger ("FadeOut");
	}
}
