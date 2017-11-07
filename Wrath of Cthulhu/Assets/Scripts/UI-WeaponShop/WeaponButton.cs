using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponButton : MonoBehaviour
{

    Player controlscript;
    public Transform PageCount; //UI element
    public GameObject ItemUI;
    public GameObject elixirImage; //elixir image
    public GameObject eyeImage; //eye image
	public GameObject reviveImage; //revive image
    Currency pagescript;
    private GameObject Mark;
    public float buySpeedCount;
    private float buyDamageCount;
    public int weaponNumber = 1;
    public Text name;
    public Text cost;
    public Text description;
    private bool boughtShotgun;
    // Use this for initialization
    void Start()
    {
        Mark = GameObject.FindWithTag("Player");
        controlscript = Mark.GetComponent<Player>();
        PageCount = GameObject.Find("PageCount").transform; //UI Element
        ItemUI = GameObject.Find("Item");
        elixirImage = ItemUI.transform.Find("ElixirUI").gameObject;
        eyeImage = ItemUI.transform.Find("EyeUI").gameObject;
		reviveImage = ItemUI.transform.Find ("ReviveUI").gameObject;
        pagescript = PageCount.GetComponent<Currency>();
        boughtShotgun = false;
        buySpeedCount = 0f;
        buyDamageCount = 0f;
        SetButton();

    }

    void SetButton()
    {
        string costString = controlscript.weapons[weaponNumber].cost.ToString();
        //name.text = controlscript.weapons[weaponNumber].weaponName;
        cost.text = "$" + costString;
        //description.text = controlscript.weapons[weaponNumber].description;
    }

    public void OnClick()
    {

		if (pagescript.count >= controlscript.weapons[weaponNumber].cost)// && controlscript.currentWeapon != weaponNumber) // You can afford it and don't have it
        {
			switch (controlscript.weapons [weaponNumber].itemCode) {
			case "elixir":
				{
					if (!controlscript.elixir) { //if don't own elixir
						pagescript.count -= controlscript.weapons [weaponNumber].cost;
						controlscript.latestBuy = weaponNumber;
						elixirImage.SetActive(true);
						eyeImage.SetActive(false);
						reviveImage.SetActive(false);
                        controlscript.elixir = true;
						controlscript.blink = false;
						controlscript.revive = false;
					}

					break;
				}
			case "blink": 
				{
					if (!controlscript.blink) { // if don't own eye
						pagescript.count -= controlscript.weapons [weaponNumber].cost;
						controlscript.latestBuy = weaponNumber;
                        elixirImage.SetActive(false);
						eyeImage.SetActive(true);
						reviveImage.SetActive(false);
                        controlscript.elixir = false;
						controlscript.blink = true;
						controlscript.revive = false;
					} 
					break;
				}
			case "revive": 
				{
					if (!controlscript.revive) { // if don't own revive
						pagescript.count -= controlscript.weapons [weaponNumber].cost;
						controlscript.latestBuy = weaponNumber;
						eyeImage.SetActive(false);
						elixirImage.SetActive(false);
						reviveImage.SetActive(true);
						controlscript.elixir = false;
						controlscript.blink = false;
						controlscript.revive = true;
					} 
					break;
				}
			case "morphine":
				{
					if (controlscript.playerMadness > 0) {
						controlscript.playerMadness = 0;
						pagescript.count -= controlscript.weapons [weaponNumber].cost;
						controlscript.latestBuy = weaponNumber;
					}
					break;
				}
			case "shotgun":
				{
					if (boughtShotgun == false) { // if shotgun upgrade and havent bought yet
						pagescript.count -= controlscript.weapons [weaponNumber].cost;
						controlscript.latestBuy = weaponNumber;
                        controlscript.bulletDamage = 60f + controlscript.damageUpgrade;
						boughtShotgun = true; //now bought
						controlscript.shotgun = true;
					} 
					break;
				}
			case "speed":
				{
					if (buySpeedCount != 3) //if 3 buys not completed yet
					{
						pagescript.count -= controlscript.weapons[weaponNumber].cost;
						controlscript.latestBuy = weaponNumber;
						controlscript.maxSpeed += .17f;                        

						if (buySpeedCount != 2)
						{
							controlscript.weapons[weaponNumber].cost += 3;
							SetButton();
						}
                        controlscript.speedCount = buySpeedCount;
                        buySpeedCount++;
					}
					break;
				}
			case "damage":
				{
					if (buyDamageCount != 3) //if 3 buys not completed yet
					{
						pagescript.count -= controlscript.weapons[weaponNumber].cost;
						controlscript.latestBuy = weaponNumber;

                        controlscript.damageUpgrade += 50f;
                        if (controlscript.shotgun == true)
                            {
                                controlscript.bulletDamage = controlscript.bulletDamage + controlscript.damageUpgrade;
                            }

                        else
                            {
                                controlscript.bulletDamage = 150f + controlscript.damageUpgrade; 
                            }

						if (buyDamageCount != 2)
						{
							controlscript.weapons[weaponNumber].cost += 3;
							SetButton();
						}
						buyDamageCount++;
					}
					break;
				}
			}
        }
    }
}
