using UnityEngine;

public class Weapon : MonoBehaviour, IDamageDealer
{
    //protected PolygonCollider2D[] weaponColliders;

    public int customDamage;
    public int Damage { get; set; }

    void Awake()
    {
        Damage = customDamage;
    }

    void Start ()
    {
        Damage = customDamage;
	}
    

}
