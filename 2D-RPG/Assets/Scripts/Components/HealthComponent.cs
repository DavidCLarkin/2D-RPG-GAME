using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float health;

    public event Action OnDie = delegate { }; //delegate to spawn particles or something, animation


    // Use this for initialization
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            OnDie();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageDealer damagedealer = collision.gameObject.GetComponentInParent<IDamageDealer>();
        if(damagedealer != null)
            TakeDamage(damagedealer.Damage);
    }

}
