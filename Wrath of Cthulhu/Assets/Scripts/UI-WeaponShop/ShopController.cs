using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

    public GameObject shopPanel;
    public GameObject openShopUI;
    public GameObject scrollBar;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OpenShopUI();
        }

    }

    void OpenShopUI()
    {
        openShopUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenShop()
    {
        openShopUI.SetActive(false);
        shopPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        scrollBar.SetActive(false);
        Time.timeScale = 1;
    }

    public void CloseOpenShopUI()
    {
        openShopUI.SetActive(false);
        Time.timeScale = 1;
    }
}
