using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour {

    public bool useItem;
    public bool pause;

	public void UseItem()
    {
        useItem = true;
    }

    public void pauseGame()
    {
        pause = true;
    }

}
