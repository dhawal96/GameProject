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

	private Vector3 enposition;

	// Use this for initialization
	void Start () {
		GameObject itemOneObject = Instantiate(items[0], ItemOne.position, ItemOne.rotation) as GameObject;
		itemOneObject.transform.SetParent (transform, true);
		itemOneObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		GameObject itemTwoObject = Instantiate(items[1], ItemTwo.position, ItemTwo.rotation) as GameObject;
		itemTwoObject.transform.SetParent (transform, true);
		itemTwoObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		GameObject itemThreeObject = Instantiate(items[2], ItemThree.position, ItemThree.rotation) as GameObject;
		itemThreeObject.transform.SetParent (transform, true);
		itemThreeObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		Time.timeScale = 0f;
		/*Player = GameObject.Find ("Player").transform;
		playerscript = Player.GetComponent <Player> ();


		enposition = gameObject.GetComponent<EnemyMove>().transform.position;
		/*if (randomIndex <= 1f && randomIndex >= 1f)
        {
            //itemIndex = Random.Range(0, collision.gameObject.GetComponent<EnemyMove>().items.Length - 1);
            GameObject coin = Instantiate(gameObject.GetComponent<EnemyMove>().items[3], enposition, Quaternion.identity);
            Destroy(coin, 5);
        }

        else
        {
		current = Random.Range(0, gameObject.GetComponent<EnemyMove>().items.Length - 1);
		Instantiate(gameObject.GetComponent<EnemyMove>().items[current], enposition, Quaternion.identity);*/
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

	void SelectItem(int selectIndex) {
		playerscript.item = items [selectIndex].name;
	}
}
