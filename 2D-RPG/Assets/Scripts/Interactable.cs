using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour 
{
	public float radius = 3f;

	bool isFocus = false;
	bool hasInteracted = false;
	public Text tooltip;
	Transform player;

	public virtual void Interact()
	{
		//Debug.Log ("Interacting with" + transform.name);
	}
		
	void Start () 
	{
		tooltip.gameObject.SetActive (false);
	}

	void Update () 
	{
		if (isFocus && !hasInteracted) 
		{
			float distance = Vector2.Distance (player.position, transform.position);
			if (distance <= radius)
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
		tooltip.text = transform.name;
		tooltip.gameObject.SetActive (true);
	}

	void OnMouseExit()
	{
		tooltip.gameObject.SetActive (false);
	}
}
