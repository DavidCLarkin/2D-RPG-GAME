using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    
	// Use this for initialization
	public void Start ()
    {
        base.Start();
        ATTACK_DELAY = 1f;
        ATTACK_TIMER = ATTACK_DELAY;
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();
	}
}
