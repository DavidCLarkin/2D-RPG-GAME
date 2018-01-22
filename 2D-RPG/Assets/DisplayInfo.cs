using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInfo : MonoBehaviour 
{
	void Update () 
	{
		if(GameManagerSingleton.instance.tooltip.IsActive())
			transform.position = Input.mousePosition;
	}
}
