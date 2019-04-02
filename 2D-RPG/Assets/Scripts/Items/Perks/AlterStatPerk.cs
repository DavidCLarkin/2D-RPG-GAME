using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Perk", menuName = "Alter Stat Perk")]
public class AlterStatPerk : Perk
{
    public int percentAmount;
    public StatToChange stat;
    public IncreaseOrDecrease choice;

    public override void EvaluatePerkStats()
    {
        HealthComponent playerHealth = GameManagerSingleton.instance.player.GetComponent<HealthComponent>();
        MovementComponent playerStamina = GameManagerSingleton.instance.player.GetComponent<MovementComponent>();

        if (choice == IncreaseOrDecrease.Increase)
        {
            switch(stat)
            {
                case StatToChange.Health:
                    playerHealth.maxHealth += (percentAmount * playerHealth.maxHealth) / 100;
                    break;
                case StatToChange.Stamina:
                    playerStamina.maxStamina += (percentAmount * playerStamina.maxStamina) / 100;
                    break;
                case StatToChange.MovementSpeed:
                    playerStamina.speed += (percentAmount * playerStamina.speed) / 100;
                    break;
            }
        }
        else if(choice == IncreaseOrDecrease.Decrease)
        {
            switch(stat)
            {
                case StatToChange.Health:
                    playerHealth.maxHealth -= (percentAmount * playerHealth.maxHealth) / 100;
                    break;
                case StatToChange.Stamina:
                    playerStamina.maxStamina -= (percentAmount * playerStamina.maxStamina) / 100;
                    break;
                case StatToChange.MovementSpeed:
                    playerStamina.speed -= (percentAmount * playerStamina.speed) / 100;
                    break;
            }
        }


    }

}
