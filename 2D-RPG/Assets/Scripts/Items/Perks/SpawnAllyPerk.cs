using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Perk", menuName = "Area Of Effect Perk")]
public class SpawnAllyPerk : Perk
{
    public int damage;
    public GameObject minion;
    private Vector2[] positions;
    private Transform player;

    public override void TriggerPerkAbility()
    {
        if (Random.Range(0, 100) > 50) // 50% chance
        {
            player = GameManagerSingleton.instance.player.transform;

            positions = new Vector2[4];
            positions[0] = new Vector2(player.position.x + Vector2.up.x * 2, player.position.y + Vector2.up.y * 2);
            positions[1] = new Vector2(player.position.x + Vector2.right.x * 2, player.position.y + Vector2.right.y * 2);
            positions[2] = new Vector2(player.position.x + Vector2.down.x * 2, player.position.y + Vector2.down.y * 2);
            positions[3] = new Vector2(player.position.x + Vector2.left.x * 2, player.position.y + Vector2.left.y * 2);

            Debug.Log("Perk triggered");
            Instantiate(minion, positions[Random.Range(0, positions.Length)], Quaternion.identity);
            minion.GetComponent<AllyWisp>().target = enemyToAffect;
        }
    }
}
