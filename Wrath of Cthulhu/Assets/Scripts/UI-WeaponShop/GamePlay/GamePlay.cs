using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    public GameObject gamePlayPanel;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickNext()
    {
        gamePlayPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
