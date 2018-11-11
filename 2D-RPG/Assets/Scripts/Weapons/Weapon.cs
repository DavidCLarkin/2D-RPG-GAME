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
    // Use this for initialization
    void Start ()
    {
        Damage = customDamage;
        //player = GameManagerSingleton.instance.player.transform;
        //weaponColliders = GetComponentsInChildren<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	public void Update ()
    {
        //Debug.Log(weaponColliders);
	}

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(player.tag))
        {
            Debug.Log("Collided");
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable.TakeDamage(Damage);
        }
    }
    */
    

}
