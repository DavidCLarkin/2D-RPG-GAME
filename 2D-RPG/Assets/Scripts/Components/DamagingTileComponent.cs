using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingTileComponent : MonoBehaviour, IDamageDealer
{
    public int customDamage;
    public int Damage { get; set; }

    void Awake()
    {
        Damage = customDamage;
    }

}
