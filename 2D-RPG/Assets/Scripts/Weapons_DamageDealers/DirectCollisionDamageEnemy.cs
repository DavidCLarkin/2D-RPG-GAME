using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectCollisionDamageEnemy : MonoBehaviour, IAiDamageDealer
{
    public int Damage { get; set; }
    public int collisionDamage;

    void Awake()
    {
        Damage = collisionDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameManagerSingleton.instance.enemyTag || collision.gameObject.tag == GameManagerSingleton.instance.bossTag)
            Destroy(transform.parent.gameObject);
    }
}
