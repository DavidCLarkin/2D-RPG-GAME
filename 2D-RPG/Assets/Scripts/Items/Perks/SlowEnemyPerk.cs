using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Perk", menuName = "Slow Perk")]
public class SlowEnemyPerk : Perk
{
    public float lengthOfSlow;

    public override void TriggerPerkAbility()
    {
        if (Random.value <= 0.25f)
        {
            if (enemyToAffect)
            {
                enemyToAffect.GetComponent<Enemy>().StartCoroutine(enemyToAffect.GetComponent<Enemy>().SlowSpeed(lengthOfSlow));
            }
            else
                Debug.Log("Enemy Null");
        }
    }
}
