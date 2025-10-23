using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : BaseGun
{

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = firearmData.magSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) || (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo <= 0))
        {
            Reload();
        }
    }

    public override void Reload()
    {
        Debug.Log($"[{this.name}] RELOADING!");
        currentAmmo=firearmData.magSize;
    }

    public override void Shoot()
    {
        currentAmmo --;
        Debug.Log($"[{this.name}] SHOT 1 BULLET! \t Current ammo = {currentAmmo}");
    }
}
