using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float health;
    public bool isAI;
    public bool isBoss;

    [Tooltip("The length of the Death Animation for this enemy")]
    public float deathTimer;

    public event Action OnDie = delegate { }; //delegate to spawn particles or something, animation
    public event Action EnableSpawnRoom = delegate { }; //delegate to enable exit point


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
            health = 0;
            OnDie(); // call all delegates
            if(isBoss)
                EnableSpawnRoom(); // if it's a boss, spawn the boss room

            //if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Die"))
            //   Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            //else
            Destroy(gameObject, deathTimer);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAI)
        {
            IDamageDealer damagedealer = collision.gameObject.GetComponentInParent<IDamageDealer>();
            if (damagedealer != null)
                TakeDamage(damagedealer.Damage);
        }
        else
        {
            IAiDamageDealer aiDamageDealer = collision.gameObject.GetComponentInParent<IAiDamageDealer>();
            if (aiDamageDealer != null)
                TakeDamage(aiDamageDealer.Damage);
        }
    }

}
