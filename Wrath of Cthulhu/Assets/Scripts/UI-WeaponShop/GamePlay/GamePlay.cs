using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    public GameObject gameplayPanel;
    public GameObject storyPanel;
    public GameObject controlsPanel;
    public GameObject instructions1;
    public GameObject instructions2;


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
            controlsPanel.SetActive(false);
            instructions1.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Return) && instructions1.activeSelf)
        {
            instructions1.SetActive(false);
            instructions2.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Return) && instructions2.activeSelf)
        {
            gameplayPanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }

    public void OnClickEnterControlsPanel()
    {
        storyPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void OnClickEnterInstructions1Panel()
    {
        controlsPanel.SetActive(false);
        instructions1.SetActive(true);
    }

    public void OnClickEnterInstructions2Panel()
    {
        instructions1.SetActive(false);
        instructions2.SetActive(true);
    }

    public void OnClickRevertToStoryPanel()
    {
        controlsPanel.SetActive(false);
        storyPanel.SetActive(true);
    }

    public void OnClickRevertToControlsPanel()
    {
        instructions1.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void OnClickRevertToInstructions1()
    {
        instructions2.SetActive(false);
        instructions1.SetActive(true);
    }

    public void OnClickExit()
    {
        gameplayPanel.SetActive(false);      
        Time.timeScale = 1f;
    }
}
