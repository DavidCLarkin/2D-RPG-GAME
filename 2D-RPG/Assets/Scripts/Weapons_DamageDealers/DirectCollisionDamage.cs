using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectCollisionDamage : MonoBehaviour, IDamageDealer
{
    public int Damage { get; set; }
    public int collisionDamage;

    void Awake()
    {
        Damage = collisionDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerColliders")
            Destroy(transform.parent.gameObject);
    }

}
