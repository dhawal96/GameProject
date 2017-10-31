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
        Application.LoadLevel(0);
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

    public void OnClickResume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
