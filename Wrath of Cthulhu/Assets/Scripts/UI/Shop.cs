using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	private Animator shopAnim;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DoneShopping(){
		Time.timeScale = 1f;
	}

	// Use this for initialization
	void Awake () {
		shopAnim = GetComponent<Animator> ();
	}
	public void MenuFade(){
		shopAnim.SetTrigger ("FadeOut");
	}
}
