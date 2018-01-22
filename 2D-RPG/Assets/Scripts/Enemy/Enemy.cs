using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Interactable
{

	public float speed = 1.5f;
	private Rigidbody2D rigidbody;
	private bool moving;

	// Use this for initialization
	void Start () 
	{
		Interactable enemy = new Interactable ();
	}
	
	// Update is called once per frame
	public override void Update() 
	{
		base.Update ();
		//print ("interacted: " + hasInteracted);
		//FollowTarget (GameObject.Find ("Player").GetComponent<Transform>());
	}

	public override void Interact()
	{
		base.Interact ();
		//Debug.Log ("Gart");
	}

	public void FollowTarget(Transform target)
	{
		transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
	}
}
