using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class Perk : ScriptableObject
{
    public virtual void EvaluatePerkStats() { }
    public virtual void TriggerPerkAbility() { }
}
