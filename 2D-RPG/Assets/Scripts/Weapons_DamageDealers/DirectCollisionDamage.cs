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
        Debug.Log("Collision with: " + collision.gameObject);
        if (collision.gameObject.tag == GameManagerSingleton.instance.player.tag)
            Destroy(transform.parent.gameObject);
    }

}
