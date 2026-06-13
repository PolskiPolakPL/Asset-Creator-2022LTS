using PolskiPolakPL.Utils;
using UnityEngine;

public class MachineGun : BaseGun
{
    Timer fireTimer;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = new Timer(firearmData.FireCooldown);
        fireTimer.OnTimerElapsed += ChamberNextBullet;
        currentAmmo = firearmData.MagSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
        {
            fireTimer.Tick(Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) || (Input.GetKey(KeyCode.Mouse0) && currentAmmo <= 0))
        {
            Reload();
        }
    }

    public override void Reload()
    {
        Debug.Log($"[{this.name}] RELOADING!");
        currentAmmo = firearmData.MagSize;
    }

    public override void Shoot()
    {
        currentAmmo--;
        Debug.Log($"[{this.name}] SHOT 1 BULLET! \t Current ammo = {currentAmmo}");
        canShoot = false;
    }

    private void ChamberNextBullet()
    {
        canShoot = true;
    }
}
