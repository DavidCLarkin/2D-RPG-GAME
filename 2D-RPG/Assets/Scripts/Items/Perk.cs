using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class Perk : ScriptableObject
{
    [HideInInspector]
    public GameObject enemyToAffect;
    public virtual void EvaluatePerkStats() { }
    public virtual void TriggerPerkAbility() { }
}
