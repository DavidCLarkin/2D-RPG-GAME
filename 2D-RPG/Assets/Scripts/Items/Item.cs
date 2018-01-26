using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable 
{
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();
	}

	public override void Interact()
	{
		//base.Interact();
		Debug.Log("Added to items");
		//Add to inventory

	}
}
