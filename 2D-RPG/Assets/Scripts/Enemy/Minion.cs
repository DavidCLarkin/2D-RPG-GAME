using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Doesn't use pathfinding because its a spirit, like the ghost.
public class Minion : Enemy
{
    // Just moves towards the players position
    public override void Update()
    {
        if(Vector2.Distance(gameObject.transform.position, GameManagerSingleton.instance.player.transform.position) <= followRange)
            transform.position = Vector2.MoveTowards(transform.position, GameManagerSingleton.instance.player.transform.position, speed * 0.5f * Time.deltaTime);
    }
}
