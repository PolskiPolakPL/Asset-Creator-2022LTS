using System;
using UnityEngine;

public interface IShootable
{
    void Shoot();
}

public interface IReloadable
{
    void Reload();
}

public interface IDamageable
{
    void TakeDamage(float amount);
}

public interface IHealable
{
    void Heal(float amount);
}
