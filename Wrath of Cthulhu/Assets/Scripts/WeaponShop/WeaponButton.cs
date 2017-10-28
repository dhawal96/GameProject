using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponButton : MonoBehaviour {

    Player controlscript;
    public Transform PageCount; //UI element
    Currency pagescript;
    private GameObject Mark;
    public int weaponNumber = 1;
    public Text name;
    public Text cost;
    public Text description;
    // Use this for initialization
    void Start ()
    {
        Mark = GameObject.FindWithTag("Player");
        controlscript = Mark.GetComponent<Player>();
        PageCount = GameObject.Find("PageCount").transform; //UI Element
        pagescript = PageCount.GetComponent<Currency>();
        SetButton();

    }

    void SetButton()
    {
        string costString = controlscript.weapons[weaponNumber].cost.ToString();
        name.text = controlscript.weapons[weaponNumber].weaponName;
        cost.text = "$" + controlscript.weapons[weaponNumber].cost;
        description.text = controlscript.weapons[weaponNumber].description;
    }

    public void OnClick()
    {
        if (pagescript.count >= controlscript.weapons[weaponNumber].cost)
        {

            pagescript.count -= controlscript.weapons[weaponNumber].cost;
            controlscript.currentWeapon = weaponNumber;

        }
        else
        {
            //Do Nothing
        }
    }
}
