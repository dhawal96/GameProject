using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponButton : MonoBehaviour
{

    Player controlscript;
    public Transform PageCount; //UI element
    Currency pagescript;
    private GameObject Mark;
    private float buySpeedCount;
    private float buyDamageCount;
    public int weaponNumber = 1;
    public Text name;
    public Text cost;
    public Text description;
    private bool boughtShotgun;
    private string currentItem;
    // Use this for initialization
    void Start()
    {
        Mark = GameObject.FindWithTag("Player");
        controlscript = Mark.GetComponent<Player>();
        PageCount = GameObject.Find("PageCount").transform; //UI Element
        pagescript = PageCount.GetComponent<Currency>();
        boughtShotgun = false;
        buySpeedCount = 0f;
        buyDamageCount = 0f;
        currentItem = "null";
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

        if (pagescript.count >= controlscript.weapons[weaponNumber].cost && controlscript.currentWeapon != weaponNumber) // You can afford it and don't have it
        {
            if (weaponNumber == 5) // if speed
            {
                if (buySpeedCount != 3) //if 3 buys not completed yet
                {
                    pagescript.count -= controlscript.weapons[weaponNumber].cost;
                    controlscript.currentWeapon = weaponNumber;
                    controlscript.maxSpeed += .33f;
                    controlscript.weapons[weaponNumber].cost += 5;
                    SetButton();
                    buySpeedCount++;
                }

                else
                {
                    //Do Nothing
                }

            }

            else if (weaponNumber == 6) //if damage
            {

                if (buyDamageCount != 3) //if 3 buys not completed yet
                {
                    pagescript.count -= controlscript.weapons[weaponNumber].cost;
                    controlscript.currentWeapon = weaponNumber;
                    controlscript.bulletDamage += 50f;
                    controlscript.weapons[weaponNumber].cost += 5;
                    SetButton();
                    buyDamageCount++;
                }

                else
                {
                    //Do Nothing
                }

            }

            else //its not speed or damage
            {
                if (weaponNumber == 4 && boughtShotgun == false) // if shotgun upgrade and havent bought yet
                {
                    pagescript.count -= controlscript.weapons[weaponNumber].cost;
                    controlscript.currentWeapon = weaponNumber;
                    boughtShotgun = true; //now bought

                }

                else if (weaponNumber == 1 || weaponNumber == 2) // if elxiir or eye
                {
                    if (weaponNumber == 1 && currentItem != "elixir") //if elixir
                    {
                        pagescript.count -= controlscript.weapons[weaponNumber].cost;
                        controlscript.currentWeapon = weaponNumber;
                        currentItem = "elixir";
                    }

                    else if (weaponNumber == 2 && currentItem != "eye") // if eye
                    {
                        pagescript.count -= controlscript.weapons[weaponNumber].cost;
                        controlscript.currentWeapon = weaponNumber;
                        currentItem = "eye";
                    }

                    else
                    {
                        //Do Nothing
                    }
                }
            }

        }

        else if (pagescript.count >= controlscript.weapons[weaponNumber].cost && controlscript.currentWeapon == weaponNumber) // You can buy it and have the item
        {
            if (weaponNumber == 5) // if speed
            {
                if (buySpeedCount != 3) //if 3 buys not completed yet
                {
                    pagescript.count -= controlscript.weapons[weaponNumber].cost;
                    controlscript.currentWeapon = weaponNumber;
                    controlscript.maxSpeed += .33f;
                    controlscript.weapons[weaponNumber].cost += 5;
                    SetButton();
                    buySpeedCount++;
                }

                else
                {
                    //Do Nothing
                }

            }

            else if (weaponNumber == 6) //if damage
            {

                if (buyDamageCount != 3) //if 3 buys not completed yet
                {
                    pagescript.count -= controlscript.weapons[weaponNumber].cost;
                    controlscript.currentWeapon = weaponNumber;
                    controlscript.bulletDamage += 50f;
                    controlscript.weapons[weaponNumber].cost += 5;
                    SetButton();
                    buyDamageCount++;
                }

                else
                {
                    //Do Nothing
                }

            }

            else // its not speed or damage

            {
                // Do Nothing
            }

        }

    }
}
