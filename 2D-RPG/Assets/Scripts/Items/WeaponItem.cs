using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public int damage;

    public override void Use()
    {
        if(GameManagerSingleton.instance.player.GetComponentInChildren<PlayerWeapon>() != null)
            GameManagerSingleton.instance.player.GetComponentInChildren<PlayerWeapon>().EquipWeapon(damage);
    }
}
