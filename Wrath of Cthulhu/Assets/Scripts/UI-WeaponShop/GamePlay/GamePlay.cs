using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    public GameObject gameplayPanel;
    public GameObject storyPanel;
    public GameObject controlsPanel;
    public GameObject instructions1;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return) && storyPanel.activeSelf)
        {
            storyPanel.SetActive(false);
            controlsPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.Return) && controlsPanel.activeSelf)
        {
            controlsPanel.SetActive(false);
            instructions1.SetActive(true);
            Time.timeScale = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.Return) && instructions1.activeSelf)
        {
            gameplayPanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }

    public void OnClickEnterControlsPanel()
    {
        storyPanel.SetActive(false);
        controlsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClickEnterInstructions1Panel()
    {
        controlsPanel.SetActive(false);
        instructions1.SetActive(true);
        Time.timeScale = 0f;
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
