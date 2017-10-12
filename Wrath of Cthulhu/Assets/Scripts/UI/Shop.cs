using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	private Animator shopAnim;


	//Player
	public Transform Player;
	Player playerscript;

	//Item List
	public GameObject[] items;
	public int current = 0;

	//Items to Place
	public Transform ItemOne;
	public Transform ItemTwo;
	public Transform ItemThree;

	//Item Text
	public Transform ItemOneDescription;
	ShopItemDescription oneDesc;
	public Transform ItemTwoDescription;
	ShopItemDescription twoDesc;
	public Transform ItemThreeDescription;
	ShopItemDescription threeDesc;
	public Transform ItemOneName;
	ShopItemName oneName;
	public Transform ItemTwoName;
	ShopItemName twoName;
	public Transform ItemThreeName;
	ShopItemName threeName;


	// Use this for initialization
	void Start () {
		//Update and handle item descriptions
		ItemOneDescription = GameObject.Find ("ItemOneDescription").transform;
		ItemTwoDescription = GameObject.Find ("ItemTwoDescription").transform;
		ItemThreeDescription = GameObject.Find ("ItemThreeDescription").transform;
		oneDesc = ItemOneDescription.GetComponent<ShopItemDescription> ();
		oneDesc.cost = items [0].GetComponent<ElixirOfLife> ().cost;
		oneDesc.description = items [0].GetComponent<ElixirOfLife> ().description;
		twoDesc = ItemTwoDescription.GetComponent<ShopItemDescription> ();
		twoDesc.cost = items [1].GetComponent<EyeOfAzethoth> ().cost;
		twoDesc.description = items [1].GetComponent<EyeOfAzethoth> ().description;
		threeDesc = ItemThreeDescription.GetComponent<ShopItemDescription> ();
		threeDesc.cost = items [2].GetComponent<Morphine> ().cost;
		threeDesc.description = items [2].GetComponent<Morphine> ().description;

		//Update and handle item name
		ItemOneName = GameObject.Find ("ItemOneName").transform;
		ItemTwoName = GameObject.Find ("ItemTwoName").transform;
		ItemThreeName = GameObject.Find ("ItemThreeName").transform;
		oneName = ItemOneName.GetComponent<ShopItemName> ();
		oneName.name = "Elixir of Life";
		twoName = ItemTwoName.GetComponent<ShopItemName> ();
		twoName.name = "Eye of Azathoth";
		threeName = ItemThreeName.GetComponent<ShopItemName> ();
		threeName.name = "Morphine";

		//Create Items
		GameObject itemOneObject = Instantiate(items[0], ItemOne.position+(transform.forward*-2), ItemOne.rotation) as GameObject;
		itemOneObject.transform.SetParent (transform, true);
		itemOneObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		GameObject itemTwoObject = Instantiate(items[1], ItemTwo.position, ItemTwo.rotation) as GameObject;
		itemTwoObject.transform.SetParent (transform, true);
		itemTwoObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		GameObject itemThreeObject = Instantiate(items[2], ItemThree.position, ItemThree.rotation) as GameObject;
		itemThreeObject.transform.SetParent (transform, true);
		itemThreeObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);


		Time.timeScale = 0f;
		Player = GameObject.Find ("Mark").transform;
		playerscript = Player.GetComponent <Player> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DoneShopping(){
		Time.timeScale = 1f;
	}

	// Use this for initialization
	void Awake () {
		shopAnim = GetComponent<Animator> ();
	}
	public void MenuFade(){
		shopAnim.SetTrigger ("FadeOut");
	}

	public void SelectItem(int selectIndex) {
		if (selectIndex == 0) {
			playerscript.item = "elixir";
		}
		else if (selectIndex == 1) {
			playerscript.item = "blink";
		}
		else if (selectIndex == 2) {
			playerscript.item = "morphine";
		}
		//playerscript.item = items [selectIndex].itemName;
	}

}
