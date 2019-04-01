using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Perk", menuName = "Area Of Effect Perk")]
public class AreaOfEffectPerk : Perk
{
    public int damage;
    public GameObject minion;

    public override void TriggerPerkAbility()
    {
        Debug.Log("Perk triggered");
        Instantiate(minion, GameManagerSingleton.instance.player.transform.position, Quaternion.identity);
    }
}
