using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootandReload : MonoBehaviour {

    public bool shoot;
    public bool reload;

	public void Shoot()
    {
        shoot = true;
    }

    public void Reload()
    {
        reload = true;
    }
}
