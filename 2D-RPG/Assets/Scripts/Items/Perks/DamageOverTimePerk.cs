using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Perk", menuName = "DOT Perk")]
public class DamageOverTimePerk : Perk
{
    public int damage;

    public override void TriggerPerkAbility()
    {
        if (enemyToAffect)
            enemyToAffect.GetComponent<HealthComponent>().StartCoroutine(enemyToAffect.GetComponent<HealthComponent>().ApplyDOT(damage));
        else
            Debug.Log("Enemy Null");
    }


}
