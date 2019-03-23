using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float health;

    public float Health
    {
        get { return health; }
        set { health += value;
            if (health >= maxHealth) health = maxHealth;
        }
    }

    public bool isAI;
    public bool isBoss;
    public GameObject damageNotifier;

    [Tooltip("The length of the Death Animation for this enemy")]
    public float deathTimer;

    public event Action OnDie = delegate { }; //delegate to spawn particles or something, animation
    public event Action EnableSpawnRoom = delegate { }; //delegate to enable exit point


    void Update()
    {
        if (health >= maxHealth)
            health = maxHealth;
    }

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

        if(damageNotifier != null)
            ShowDamageNotifier(damageAmount);
    }

    void ShowDamageNotifier(int damageAmount)
    {
        GameObject damageNotifierClone = damageNotifier;
        damageNotifierClone.GetComponent<TextMesh>().text = damageAmount.ToString();
        damageNotifierClone.GetComponent<TextMesh>().color = isAI ? Color.red : Color.green;

        Instantiate(damageNotifierClone, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z - 1), Quaternion.identity, transform);
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
