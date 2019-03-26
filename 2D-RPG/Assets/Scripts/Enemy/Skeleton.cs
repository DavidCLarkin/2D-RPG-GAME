using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    
	// Use this for initialization
	public void Start ()
    {
        base.Start();
        //ATTACK_DELAY = 1f; set in inspector
        ATTACK_TIMER = ATTACK_COOLDOWN;
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();
	}
}
