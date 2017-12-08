using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public GameObject pausePanel;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickPlayAgain()
    {
        Destroy(GameObject.FindGameObjectWithTag("Music"));
        Application.LoadLevel(0);
        GameObject.FindGameObjectWithTag("Music").GetComponent<MainMenuMusic>().PlayMainMenuMusic();
    }

    public void OnClickExitGame()
    {
        Application.OpenURL("https://itch.io/");
    }

    public void OnClickResume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickRestart()
    {
        Destroy(GameObject.FindGameObjectWithTag("Music"));
        Time.timeScale = 1f;
        Application.LoadLevel(0);
        GameObject.FindGameObjectWithTag("Music").GetComponent<MainMenuMusic>().PlayMainMenuMusic();
    }
}
