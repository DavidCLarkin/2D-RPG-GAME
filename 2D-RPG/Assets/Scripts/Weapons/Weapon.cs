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
<<<<<<< HEAD

    void Start ()
    {
        Damage = customDamage;
	}
    
=======
>>>>>>> 41680df57ad1705639630fbc954845fbe358b5ef

}
