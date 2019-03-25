using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour, IAiDamageDealer
{
    //Changeable damage specific to weapon
    public int customDamage;
    public int Damage { get; set; }

    public Text weaponInfo;

	// Use this for initialization
	void Awake ()
    {
        Damage = customDamage;
	}

    public void EquipWeapon(int newDamage)
    {
        Damage = newDamage;
    }

    void Update()
    {
        weaponInfo.text = "Weapon Damage: " + Damage;
    }

}
