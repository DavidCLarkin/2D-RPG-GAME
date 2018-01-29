﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
	public float radius = 3f;
	protected bool isFocus = false; 
	protected bool hasInteracted = false;
	public Transform player;

	public virtual void Interact()
	{
		//Debug.Log ("Interacting with " + transform.name);
	}

	void Start() 
	{
	}

	public virtual void Update() 
	{
		if(isFocus && !hasInteracted) 
		{
			float distance = Vector2.Distance (player.position, transform.position);
			if(distance <= radius)
			{
				Interact();
				hasInteracted = true;
			}
		}
	}

	public void onFocused(Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}
		
	public void deFocused()
	{
		isFocus = false;
		player = null;
		hasInteracted = false;
	}

	void OnMouseEnter()
	{
		GameManagerSingleton.instance.tooltip.text = transform.name;
		GameManagerSingleton.instance.tooltip.gameObject.SetActive(true);
	}

	void OnMouseExit()
	{
		GameManagerSingleton.instance.tooltip.gameObject.SetActive(false);
	}
}