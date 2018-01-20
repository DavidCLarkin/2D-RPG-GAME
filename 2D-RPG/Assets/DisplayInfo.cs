using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInfo : MonoBehaviour 
{
	void Update () 
	{
		transform.position = Input.mousePosition;
	}
}
