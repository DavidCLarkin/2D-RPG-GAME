using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour, IAiDamageDealer
{
    //Changeable damage specific to weapon
    public int customDamage;
    public int Damage { get; set; }

    public Text weaponInfo;
    public WeaponItem equippedWeapon;

	// Use this for initialization
	void Awake ()
    {
        Damage = customDamage;
        UpdateUI();
	}

    /*
     * Method to equip a weapon 
     * Sets the damage number to the equipped weapons damage, plays a sound and evaluates perks
     * (changes health/stamina/movement speed accordingly)
     */ 
    public void EquipWeapon(WeaponItem weapon)
    {
        if(equippedWeapon)
            UnequipWeapon(equippedWeapon);
        // Check in-game DB to equip the right weapon via it's ID
        foreach (GameObject item in GameManagerSingleton.instance.GetComponent<ItemDatabase>().items)
            if (weapon.itemID == item.GetComponent<Item>().itemID)
                equippedWeapon = item.GetComponent<WeaponItem>();

        Damage = equippedWeapon.damage; // set weapon damage

        SoundManager.instance.PlayRandomOneShot(SoundManager.instance.equipWeaponSounds);

        UpdateUI();

        // Evaluate perk changes to the player stats
        foreach(Perk perk in equippedWeapon.perks)
        {
            perk.EvaluatePerkStats();
        }
    }

    /*
     * Unequips a weapon by setting equippedWeapon to null and updating variables.
     */ 
    public void UnequipWeapon(WeaponItem weapon)
    {
        equippedWeapon = null;
        Damage = customDamage;
        GameManagerSingleton.instance.player.GetComponent<Stats>().UpdateVariables(false); // reset stats
        GameManagerSingleton.instance.player.GetComponent<MovementComponent>().ResetSpeed();
        UpdateUI();
    }

    private void UpdateUI()
    {
        weaponInfo.text = "Weapon Damage: " + Damage;
    }

}
