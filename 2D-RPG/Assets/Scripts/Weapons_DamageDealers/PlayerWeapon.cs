using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IAiDamageDealer
{
    //Changeable damage specific to weapon
    public int customDamage;
    public int Damage { get; set; }

	// Use this for initialization
	void Awake ()
    {
        Damage = customDamage;
	}
	
}
