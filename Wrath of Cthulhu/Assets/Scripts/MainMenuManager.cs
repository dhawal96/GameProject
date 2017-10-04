using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	private Animator menuAnim;

	// Use this for initialization
	void Awake () {
		menuAnim = GetComponent<Animator> ();
	}
	public void MenuFade(){
        menuAnim.SetTrigger ("FadeOut");
    }

    public void DestroyScene()
    {
        SceneManager.UnloadScene("MainMenu");

    }
	public void LoadScene () {
        SceneManager.LoadScene("PlayerMovement", LoadSceneMode.Single);
        
        //gameObject.transform.GetChild(2).gameObject.Destro(false);
        //System.Threading.Thread.Sleep(1000);
    }
}
