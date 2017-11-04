using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    public GameObject gameplayPanel;
    public GameObject storyPanel;
    public GameObject controlsPanel;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return) && storyPanel.activeSelf)
        {
            storyPanel.SetActive(false);
            controlsPanel.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Return) && controlsPanel.activeSelf)
        {
            gameplayPanel.SetActive(false);
            Time.timeScale = 1f;
        }
		
	}

    public void OnClickEnterControlsPanel()
    {
        storyPanel.SetActive(false);
        controlsPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OnClickExit()
    {
        if (storyPanel.activeSelf)
        {
            storyPanel.SetActive(false);
        }

        if (controlsPanel.activeSelf)
        {
            controlsPanel.SetActive(false);
        }

        gameplayPanel.SetActive(false);
        
        Time.timeScale = 1f;
    }
}
