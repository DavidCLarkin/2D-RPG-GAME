using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CONSUME_TYPE { Health, Stamina };

public class ConsumableItem : Item
{
    public int amount;
    public CONSUME_TYPE potionType;

    public override void Use()
    {
        if (potionType == CONSUME_TYPE.Health)
        {
            GameManagerSingleton.instance.player.GetComponent<HealthComponent>().health += amount;
            Debug.Log("Increased Health");
        }
        if (potionType == CONSUME_TYPE.Stamina)
        {
            GameManagerSingleton.instance.player.GetComponent<MovementComponent>().stamina += amount;
            Debug.Log("Increase Stamina");
        }
    }

}
