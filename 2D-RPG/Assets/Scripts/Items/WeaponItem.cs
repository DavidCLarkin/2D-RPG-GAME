using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public int damage;
    public Perk[] perks;

    public override void Use()
    {
        if (GameManagerSingleton.instance.player.GetComponentInChildren<PlayerWeapon>() != null)
        {
            GameManagerSingleton.instance.player.GetComponentInChildren<PlayerWeapon>().EquipWeapon(this);
        }
        //Debug.Log("OVERIDDING");
    }

    
}
