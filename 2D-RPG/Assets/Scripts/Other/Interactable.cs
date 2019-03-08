using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
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
        player = GameManagerSingleton.instance.player.transform;
	}

	public virtual void Update() 
	{
		if(isFocus && !hasInteracted) 
		{
			float distance = Vector2.Distance (player.position, transform.position);
			if(distance <= radius)
			{
				//Interact();
				hasInteracted = true;
			}
		}
	}

	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		//player = playerTransform;
		hasInteracted = false;
	}
		
	public void DeFocused()
	{
		isFocus = false;
		//player = null;
		hasInteracted = false;
	}

    protected virtual void EnableTooltip()
    {
        // Need to set the text in corresponding Interactable
        GameManagerSingleton.instance.tooltip.gameObject.SetActive(true);
        GameManagerSingleton.instance.tooltip.gameObject.GetComponent<DisplayInfo>().SetPosition(gameObject.transform);
    }

    protected virtual void DisableTooltip()
    {
        GameManagerSingleton.instance.tooltip.gameObject.SetActive(false);
    }

}
