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

    public void EquipWeapon(WeaponItem weapon)
    {
        if(equippedWeapon)
            UnequipWeapon(equippedWeapon);

        foreach (GameObject item in GameManagerSingleton.instance.GetComponent<ItemDatabase>().items)
            if (weapon.itemID == item.GetComponent<Item>().itemID)
                equippedWeapon = item.GetComponent<WeaponItem>();

        Damage = equippedWeapon.damage;

        UpdateUI();

        foreach(Perk perk in equippedWeapon.perks)
        {
            perk.EvaluatePerkStats();
        }
    }

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
