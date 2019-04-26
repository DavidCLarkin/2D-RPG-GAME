using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatToChange { Health, Stamina, MovementSpeed }; // choose which stat to alter
public enum IncreaseOrDecrease { Increase, Decrease }; // increase or decrease the stat chosen

public abstract class Perk : ScriptableObject
{
    [HideInInspector]
    public GameObject enemyToAffect; // enemy that is to be affected if applicable
    public virtual void EvaluatePerkStats() { } // evaluate perk stats i.e. change the player's health/stamina/movement speed as per StatToChange enum
    public virtual void TriggerPerkAbility() { } // trigger the perk on a weapon for example slow enemy, apply damage over time etc
}
