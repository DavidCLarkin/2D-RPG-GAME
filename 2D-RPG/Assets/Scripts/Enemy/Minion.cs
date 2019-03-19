using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Doesn't use pathfinding because its a spirit, like the ghost.
public class Minion : MonoBehaviour
{
    public float followRange;
    public float speed;

    private void FixedUpdate()
    {
        if(Vector2.Distance(gameObject.transform.position, GameManagerSingleton.instance.player.transform.position) <= followRange)
            transform.position = Vector2.MoveTowards(transform.position, GameManagerSingleton.instance.player.transform.position, speed * 0.5f * Time.deltaTime);
    }
}
/*
public class Minion : Enemy
{
    // Use this for initialization
    void Start()
    {
        base.Start();
        pathfinding.target = player;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void DistanceChecking()
    {
        distance = Vector2.Distance(player.position, transform.position);
        if (distance < followRange)
        {
            state = State.Moving;
        }
    }
}
*/
