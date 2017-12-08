using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEnterButton : MonoBehaviour
{
    public Button button;
    public GameObject menu;
    private bool executeOnce;
    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();
        executeOnce = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !executeOnce && menu.GetComponent<MainMenuManager>().menuFaded == false)
        {
            button.onClick.Invoke();
            executeOnce = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
}
	

