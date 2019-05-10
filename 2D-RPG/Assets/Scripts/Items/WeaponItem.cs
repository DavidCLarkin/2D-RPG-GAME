using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Simple class to hold damage and perk data of a weapon. 
 * Also equips a weapon to PlayerWeapon class upon use
 */ 
public class WeaponItem : Item
{
    public int damage;
    public Perk[] perks; // list of perks the weapon has - can vary for flexibility

    /*
     * Overridden Use method that equips a weapon when a weapon is clicked in inventory slot
     */
    public override void Use()
    {
       if (player.GetComponentInChildren<PlayerWeapon>() != null)
       {
            player.GetComponentInChildren<PlayerWeapon>().EquipWeapon(this);
       }
    }
}
