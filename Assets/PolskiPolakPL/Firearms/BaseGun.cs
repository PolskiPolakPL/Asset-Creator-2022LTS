using UnityEngine;
public abstract class BaseGun : MonoBehaviour, IShootable, IReloadable
{
    [SerializeField] protected FirearmData firearmData;
    [SerializeField] protected Transform muzzle;
    protected int currentAmmo;

    public abstract void Shoot();

    public abstract void Reload();
}
