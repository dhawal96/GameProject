using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	private Animator menuAnim;
	private CanvasGroup menuCanvasGroup;
    public bool menuFaded;

    // Use this for initialization
    void Awake () {
		menuAnim = GetComponent<Animator> ();
		menuCanvasGroup = GetComponent<CanvasGroup> ();
        menuFaded = false;
	}
	public void MenuFade(){
        menuAnim.SetTrigger ("FadeOut");
        menuFaded = true;
    }

	public void MenuHide(){
		menuCanvasGroup.alpha = 0;
	}
		
	public void LoadScene () {
		SceneManager.LoadScene("PlayerMovement", LoadSceneMode.Additive);
    }
}
